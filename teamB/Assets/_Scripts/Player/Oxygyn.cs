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
        GameObject[] subPlayers = GameObject.FindGameObjectsWithTag("SubPlayer");
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        float distEnemy = Vector2.Distance(transform.position, enemyTr.position);

        // =================================================
        // Enemy Å® Escape
        // =================================================
        if ((state == State.Idle || state == State.Follow) && distEnemy < 1.3f)
        {
            state = State.Escape;

            escapeTime = 1.0f;

            randomx = Random.Range(-9f, 9f);
            randomy = Random.Range(-4.5f, 4.5f);

            target = null;
        }

        // =================================================
        // Escape
        // =================================================
        if (state == State.Escape)
        {
            escapeTime -= Time.deltaTime;

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(randomx, randomy, -6f),
                speed * Time.deltaTime
            );

            if (escapeTime <= 0f)
            {
                state = State.Idle;
            }

            return;
        }

        // =================================================
        // Idle Å® çıìGÅiÇ±Ç±Ç™çƒí«è]ÇÃåÆÅj
        // =================================================
        if (state == State.Idle)
        {
            IOxygenTarget bestTarget = null;
            float bestDist = Mathf.Infinity;

            float range = 4.0f;

            // Player
            if (player.CanGetOxygen())
            {
                float d = Vector2.Distance(transform.position, player.transform.position);

                if (d < range)
                {
                    bestTarget = player;
                    bestDist = d;
                }
            }

            // SubPlayer
            foreach (GameObject sp in subPlayers)
            {
                SubPlayer sub = sp.GetComponent<SubPlayer>();
                if (sub == null) continue;

                if (!sub.CanGetOxygen()) continue;

                float d = Vector2.Distance(transform.position, sp.transform.position);

                if (d < range && d < bestDist)
                {
                    bestTarget = sub;
                    bestDist = d;
                }
            }

            // í«è]äJén
            if (bestTarget != null)
            {
                target = bestTarget;
                target.AddOxygen();
                state = State.Follow;
            }
        }

        // =================================================
        // Follow
        // =================================================
        if (state == State.Follow && target != null)
        {
            MonoBehaviour mb = target as MonoBehaviour;

            if (mb == null)
            {
                target = null;
                state = State.Idle;
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(mb.transform.position.x, mb.transform.position.y, -6f),
                speed * Time.deltaTime
            );
        }
    }
}