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

    void FixedUpdate()
    {
        if (playerTr == null) return;

        float dist = Vector2.Distance(transform.position, playerTr.position);

        if (dist > 1f)
        {
            Vector2 dir = ((Vector2)playerTr.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
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