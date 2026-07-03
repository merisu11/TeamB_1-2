using UnityEngine;

public class SubPlayer : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;//SubPlayerが持ってる酸素の数
    public static int maxOxygen = 1;//SubPlayerが持てる酸素の数

    [SerializeField] float resetTime = 3f;
    private float resetTimer;

    Transform playerTr;
    public float speed = 5f;
    bool Follow = true;

    private bool playerLost = false;
    private Vector3 touchWorldPosition; // Player不在時（ゴール後）にマウス操作する際の目標位置
    private Rigidbody2D rb;//0703サブプレイヤーの挙動修正

    public int slotIndex = 0;
    public int slotTotal = 1;
   [SerializeField] float formationRadius = 1.2f; // Playerからどれくらい離れて並ぶか
    [SerializeField] float separationRadius = 0.8f;   // これより近いSubPlayerから離れる
    [SerializeField] float separationWeight = 3f;      // 分離の強さ
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//挙動修正
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTr = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Playerが見つかりません"); // ← これが出るか確認
        }
           
        resetTimer = resetTime;
        touchWorldPosition = transform.position;
    }

    Vector2 GetFormationTarget()
    {
        float angle = (360f / slotTotal) * slotIndex * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * formationRadius;
        return (Vector2)playerTr.position + offset;
    }


    Vector2 GetSeparationForce()
    {
        Vector2 separation = Vector2.zero;
        GameObject[] others = GameObject.FindGameObjectsWithTag("SubPlayer");

        foreach (GameObject other in others)
        {
            if (other == gameObject) continue; // 自分自身はスキップ

            float dist = Vector2.Distance(transform.position, other.transform.position);

            if (dist < separationRadius && dist > 0.0001f)
            {
                Vector2 away = (Vector2)(transform.position - other.transform.position);
                // 近いほど強く反発させる（距離で割る）
                separation += away.normalized / dist;
            }
        }
        return separation;
    }

    void FixedUpdate()
    {
        if (playerTr == null) return;

        Vector2 target = GetFormationTarget();
        Vector2 toTarget = target - (Vector2)transform.position;

        Vector2 seekVelocity = Vector2.zero;
        if (toTarget.magnitude > 0.2f)
        {
            seekVelocity = toTarget.normalized * speed;
        }

        Vector2 separationForce = GetSeparationForce() * separationWeight;

        // 目標に向かう速度 + 分離の力を合成する
        Vector2 finalVelocity = seekVelocity + separationForce;

        // 速度が出すぎないようにクランプ
        if (finalVelocity.magnitude > speed)
        {
            finalVelocity = finalVelocity.normalized * speed;
        }

        rb.linearVelocity = finalVelocity;
    }

    void Update()
    {
        if (playerTr != null)
        {
            float s = Vector2.Distance(transform.position, playerTr.position);
            Debug.Log($"距離: {s}");
        }
        // Playerがゴールしてシーンから消えている場合は、SubPlayer自身をマウスクリックで操作する
        // （Player.csのクリック移動と同じ仕組み）
        if (playerTr == null)
        {
            // playerTrがnullになった瞬間だけ現在地で止める
            if (!playerLost)
            {
                playerLost = true;
                touchWorldPosition = transform.position; // ← これがないとワープする
                rb.linearVelocity = Vector2.zero; // ← 止める
            }

            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 5.0f;
                touchWorldPosition = Camera.main.ScreenToWorldPoint(pos);
            }

            transform.position = Vector3.MoveTowards(transform.position, touchWorldPosition, speed * Time.deltaTime);
            return;
        }
       

    }

    public bool TryGetOxygen()
    {
        if (oxygenCount >= maxOxygen)
        {
            return false;
        }

        oxygenCount++;
        resetTimer = resetTime;
        return true;
    }

    public void Reset()
    {
        oxygenCount = 0;
    }
}