using UnityEngine;

public class EnemyMove : MonoBehaviour, IPathogen
{
    [SerializeField] private float speed = 3f;

    private Transform target;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private const float StunDuration = 2.0f;

    private bool isImpeded = false;
    private Rigidbody2D rb; // ← rb を宣言

    // IPathogen の実装（白血球に張り付かれたとき呼ばれる）
    public void SetImpeded(bool impeded)
    {
        isImpeded = impeded;
        rb.linearVelocity = Vector2.zero;
        // Kinematicにして物理的にも完全に止める
        rb.bodyType = impeded ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ← Startで取得
        FindNearestPlayer();
    }

    private void Update()
    {
        if (isImpeded) return;

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                FindNearestPlayer();
            }
            return;
        }

        if (target == null)
        {
            FindNearestPlayer();
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position, target.position, speed * Time.deltaTime);
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStunned) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            isStunned = true;
            stunTimer = StunDuration;
        }
    }
}