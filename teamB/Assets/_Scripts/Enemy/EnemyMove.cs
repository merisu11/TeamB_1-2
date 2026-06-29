using UnityEngine;

public class EnemyMove : MonoBehaviour, IPathogen
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float StunDuration = 2.0f;

    private Transform target;
    private bool isStunned = false;
    private float stunTimer = 0f;

    // static を外してインスタンスごとに管理する
    // static だと全ての敵が同じフラグを共有してしまうため、1体が捕まると全員止まるバグが起きる
    // インスタンスごとに管理する（static にすると1体捕まると全員止まるバグが起きる）
    private bool _isImpeded = false;

    // Oxygyn.cs など外部から EnemyMove.isImpeded でアクセスできるよう互換性を綴持する
   
    public static bool isImpeded
    {
        get
        {
            foreach (EnemyMove e in FindObjectsByType<EnemyMove>(FindObjectsSortMode.None))
                if (e._isImpeded) return true;
            return false;
        }
    }
    private Rigidbody2D rb;

    public bool IsImpeded => _isImpeded;

    public void SetImpeded(bool impeded)
    {
        _isImpeded = impeded;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = impeded ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindNearestPlayer();
    }

    private void Update()
    {
        if (_isImpeded) return;

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