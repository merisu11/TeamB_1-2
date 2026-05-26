using UnityEngine;

/// <summary>
/// 白血球クラス
/// 仕様：病原菌を検知したら近づいて張り付き行動を阻害する
/// </summary>
public class hakekkyuu : MonoBehaviour
{
    public enum State
    {
        Follow,
        Chase,
        Attach,
    }

    [Header("検知・妨害")]
    [Tooltip("基本の検知範囲（キャラクターサイズの2倍に設定してください）")]
    public float detectionRange = 5f;

    [Tooltip("基本の妨害継続時間（秒）/ スキル未取得: 2秒")]
    public float attachDuration = 2f;

    [Header("移動")]
    public float moveSpeed = 3f;

    [Tooltip("追従時、赤血球との目標距離")]
    public float followDistance = 1.5f;

    [Header("集中阻止")]
    [Tooltip("1体の病原菌に同時に張り付けるホワイトセルの最大数")]
    public int maxPerPathogen = 2;

    // ================= スキルによる補正値 =================
    // スキルツリー側からこれらのメソッドを呼び出してください

    
    /// 【スキル①】抑えられる時間をのばす
    
   
    public void SetAttachDuration(float duration)
    {
        attachDuration = duration;
    }

   
    /// 【スキル②】ウイルスを感知する範囲を広げる
    /// スキルツリー側から呼び出してください
    
    public void SetDetectionRangeMultiplier(float multiplier)
    {
        detectionRange = baseDetectionRange * multiplier;
    }

    // ================= 内部変数 =================
    private State currentState = State.Follow;
    private Transform playerTransform;
    private Transform chaseTarget;
    private GameObject attachedPathogen;
    private float attachTimer;
    private Rigidbody2D rb;
    private Collider2D col;
    private float baseDetectionRange; // 初期の検知範囲を保存

    // ================= Unity ライフサイクル =================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // 初期の検知範囲を保存（スキル倍率計算の基準値）
        baseDetectionRange = detectionRange;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogWarning("[白血球] タグ'Player'のオブジェクトが見つかりません");

        currentState = State.Follow;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Follow:
                UpdateFollow();
                SearchPathogen();
                break;
            case State.Chase:
                UpdateChase();
                break;
            case State.Attach:
                UpdateAttach();
                break;
        }
    }

    // ================= 追従（Follow） =================
    void UpdateFollow()
    {
        if (playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);
        if (dist > followDistance)
        {
            Vector2 dir = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // ================= 病原菌の検知 =================
    void SearchPathogen()
    {
        Collider2D[] allHits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (Collider2D hit in allHits)
        {
            if (hit.gameObject == gameObject) continue;
            if (LayerMask.LayerToName(hit.gameObject.layer) != "Pathogen") continue;

            int attached = CountAttachedTo(hit.gameObject);
            if (attached < maxPerPathogen)
            {
                chaseTarget = hit.transform;
                currentState = State.Chase;
                return;
            }
        }
    }

    // ================= 追いかける（Chase） =================
    void UpdateChase()
    {
        if (chaseTarget == null)
        {
            currentState = State.Follow;
            return;
        }

        Vector2 dir = ((Vector2)chaseTarget.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    // Chase中に病原菌に接触したらAttach開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != State.Chase) return;
        if (chaseTarget == null) return;
        if (collision.gameObject != chaseTarget.gameObject) return;

        BeginAttach(collision.gameObject);
    }

    // ================= 張り付き開始 =================
    void BeginAttach(GameObject pathogen)
    {
        attachedPathogen = pathogen;
        attachTimer = attachDuration;
        currentState = State.Attach;

        // 接触した瞬間にisTriggerをオンにして以降の押し合いを無効化
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        if (col != null) col.isTrigger = true;

        IPathogen script = pathogen.GetComponent<IPathogen>();
        if (script != null)
            script.SetImpeded(true);
        else
            Debug.LogError($"[白血球] IPathogen が見つかりません: {pathogen.name}");
    }

    // ================= 張り付き中（Attach） =================
    void UpdateAttach()
    {
        if (attachedPathogen == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = attachedPathogen.transform.position;

        attachTimer -= Time.deltaTime;
        if (attachTimer <= 0f)
        {
            IPathogen script = attachedPathogen.GetComponent<IPathogen>();
            if (script != null) script.SetImpeded(false);
            Destroy(gameObject);
        }
    }

    // ================= ユーティリティ =================
    int CountAttachedTo(GameObject pathogen)
    {
        int count = 0;
        foreach (hakekkyuu wbc in FindObjectsByType<hakekkyuu>(FindObjectsSortMode.None))
        {
            if (wbc.attachedPathogen == pathogen)
                count++;
        }
        return count;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}