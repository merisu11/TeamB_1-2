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

    [SerializeField] public static float speed = 10f;//速度

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

        // ゴール後はPlayerタグのオブジェクトがDestroyされて存在しなくなるため、
        // FindGameObjectWithTagがnullを返すことがある。nullのままGetComponentすると例外になるので確認する
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player player = playerObj != null ? playerObj.GetComponent<Player>() : null;

        float distEnemy = Vector2.Distance(transform.position, enemyTr.position);

        if ((state == State.Follow) && distEnemy < 0.8f)
        {
            state = State.Escape;

            escapeTime = 1.0f;

            randomx = Random.Range(-9f, 9f);
            randomy = Random.Range(-4.5f, 4.5f);
            target.Reset();
            //player.oxygenCount = 0;
            target = null;
        }

        if (state == State.Escape)
        {
            escapeTime -= Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position,new Vector3(randomx, randomy, -6f),speed * Time.deltaTime);

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

            float range = 1.5f;//酸素の取得範囲

            if (player != null && player.oxygenCount < Player.maxOxygen)
            {
                float d = Vector2.Distance(transform.position, player.transform.position);

                if (d < range)
                {
                    bestTarget = player;
                    bestDist = d;
                }
            }

            foreach (GameObject sp in subPlayers)
            {
                SubPlayer sub = sp.GetComponent<SubPlayer>();
                if (sub == null) continue;
                if (sub.oxygenCount >= SubPlayer.maxOxygen) continue;

                float d = Vector2.Distance(transform.position, sp.transform.position);

                if (d < range && d < bestDist)
                {
                    bestTarget = sub;
                    bestDist = d;
                }
            }

            if (bestTarget != null)
            {
                bool result = bestTarget.TryGetOxygen();;

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

            transform.position = Vector3.MoveTowards(transform.position,new Vector3(mb.transform.position.x, mb.transform.position.y, -6f),speed * Time.deltaTime);
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