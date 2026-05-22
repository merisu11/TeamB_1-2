using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Transform target;       // 現在の追跡対象
    private bool isStunned = false; // 行動不能フラグ
    private float stunTimer = 0f;
    [SerializeField] private float StunDuration = 2.0f; // 攻撃後の行動不能時間

    private void Start()
    {
        FindNearestPlayer();
    }

    private void Update()
    {
        // 行動不能中はタイマーを進めるだけ
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                FindNearestPlayer(); // 行動不能明けに索敵を再開
            }
            return;
        }

        // ターゲットが消えていたら再索敵
        if (target == null)
        {
            FindNearestPlayer();
            return;
        }

        // 最も近い赤血球に接近
        transform.position = Vector2.MoveTowards(
            transform.position, target.position, speed * Time.deltaTime);
    }

    // マップ上の一番近い赤血球を探す（仕様書: 一番近い赤血球に接近）
    private void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDist = float.MaxValue;
        target = null;

        foreach (GameObject p in players)
        {
            float dist = Vector2.Distance(transform.position, p.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                target = p.transform;
            }
        }
    }

    // 接触した時に攻撃判定（仕様書: 攻撃判定は接触した時）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStunned) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // 攻撃後1秒間行動不能（仕様書: 攻撃後１秒間行動不能）
            isStunned = true;
            stunTimer = StunDuration;
        }
    }
}