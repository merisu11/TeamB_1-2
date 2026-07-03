using UnityEngine;

public class Oxygyn : MonoBehaviour
{
    enum State
    {
        Idle,
        Follow,
        Escape
    }

    State state = State.Idle;

    Transform enemyTr;
    IOxygenTarget target;

    [SerializeField] public static float speed = 10f;

    private float escapeTime = 0f;
    private float randomx;
    private float randomy;

    void Start()
    {
        enemyTr = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void Update()
    {
        float distEnemy = Vector2.Distance(transform.position, enemyTr.position);

        if ((state == State.Follow) && distEnemy < 0.8f)
        {
            state = State.Escape;
            escapeTime = 1.0f;
            randomx = Random.Range(-9f, 9f);
            randomy = Random.Range(-4.5f, 4.5f);
            target.Reset();
            target = null;
        }

        if (state == State.Escape)
        {
            escapeTime -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomx, randomy, -6f), speed * Time.deltaTime);
            if (escapeTime <= 0f)
            {
                state = State.Idle;
            }
            return;
        }

        if (state == State.Idle)
        {
            IOxygenTarget bestTarget = null;
            float bestDist = Mathf.Infinity;
            float range = 0.5f;

            // Player・SubPlayer問わず、シーン内の全Playerコンポーネントを対象にする
            Player[] allPlayers = FindObjectsByType<Player>(FindObjectsSortMode.None);

            foreach (Player p in allPlayers)
            {
                if (p.oxygenCount >= Player.maxOxygen) continue;

                float d = Vector2.Distance(transform.position, p.transform.position);

                if (d < range && d < bestDist)
                {
                    bestTarget = p;
                    bestDist = d;
                }
            }

            if (bestTarget != null)
            {
                bool result = bestTarget.TryGetOxygen();

                if (result)
                {
                    target = bestTarget;
                    state = State.Follow;
                }
            }
        }

        if (state == State.Follow && target != null)
        {
            MonoBehaviour mb = target as MonoBehaviour;

            if (mb == null)
            {
                target = null;
                state = State.Idle;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(mb.transform.position.x, mb.transform.position.y, -6f), speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            escapeTime = 0;
        }
    }
}