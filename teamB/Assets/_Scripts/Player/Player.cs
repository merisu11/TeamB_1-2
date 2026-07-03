using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;
    public static int maxOxygen = 1;

    [SerializeField] float resetTime = 3f;

    public static int speed = 5;
    Vector3 touchWorldPosition;
    private float time;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite stunSprite;

    [Header("酸素獲得スプライト表示設定")]
    [Tooltip("酸素を獲得した瞬間に表示するスプライト")]
    [SerializeField] private Sprite oxygenGetSprite;

    [Tooltip("赤血球より後ろに表示するためのSorting Orderオフセット（マイナス値を指定）")]
    [SerializeField] private int oxygenGetEffectSortingOrderOffset = -1;

    [Tooltip("スプライトを表示し続ける時間（秒）")]
    [SerializeField] private float oxygenGetDisplayDuration = 0.3f;

    [Tooltip("フェードアウトにかける時間（秒）。0にすると表示時間経過後に即座に消えます")]
    [SerializeField] private float oxygenGetFadeDuration = 0.3f;

    [Header("役割設定")]
    [Tooltip("本体（クリック操作する側）ならtrue。サブプレイヤーならfalse")]
    public bool isLeader = true;

    [Header("サブプレイヤー用設定（isLeader=falseのときだけ使う）")]
    public Player leader;              // 追いかける相手（リーダー）
    public int slotIndex = 0;          // 自分が何番目か
    public int slotTotal = 1;          // 全体で何人いるか
    public float formationRadius = 1.2f; // リーダーからの距離

    // 他のスクリプトからリーダーのクリック目標地点を読めるようにする
    public Vector3 TouchWorldPosition => touchWorldPosition;

    void Start()
    {
        touchWorldPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (time < 0)
        {
            if (isLeader)
            {
                // ===== 本体（リーダー）はこれまで通りクリックで操作 =====
                if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 pos = Input.mousePosition;
                    pos.z = 5.0f;
                    touchWorldPosition = Camera.main.ScreenToWorldPoint(pos);
                }
            }
            else
            {
                // ===== サブプレイヤーはリーダーの周りの自分専用の点を目指す =====
                if (leader != null)
                {
                    touchWorldPosition = GetFormationTarget();
                }
            }

            spriteRenderer.sprite = normalSprite;
            transform.position = Vector3.MoveTowards(transform.position, touchWorldPosition, speed * Time.deltaTime);
        }

        time -= Time.deltaTime;
    }

    // リーダーを中心とした円周上の自分の目標位置を計算する
    Vector3 GetFormationTarget()
    {
        float angle = (360f / slotTotal) * slotIndex * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * formationRadius;
        return leader.transform.position + offset;
    }

    public bool TryGetOxygen()
    {
        if (oxygenCount >= maxOxygen) return false;
        oxygenCount++;
        PlayOxygenGetEffect();
        return true;
    }

    // 酸素獲得スプライトを、自分（赤血球）の後ろに表示する
    private void PlayOxygenGetEffect()
    {
        if (oxygenGetSprite == null) return;

        GameObject effectObj = new GameObject("OxygenGetEffect");
        effectObj.transform.position = transform.position;

        SpriteRenderer effectRenderer = effectObj.AddComponent<SpriteRenderer>();
        effectRenderer.sprite = oxygenGetSprite;

        // スプライトが赤血球より後ろに描画されるようSorting Orderを調整
        if (spriteRenderer != null)
        {
            effectRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
            effectRenderer.sortingOrder = spriteRenderer.sortingOrder + oxygenGetEffectSortingOrderOffset;
        }

        OxygenGetSpriteEffect spriteEffect = effectObj.AddComponent<OxygenGetSpriteEffect>();
        spriteEffect.Setup(oxygenGetDisplayDuration, oxygenGetFadeDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchWorldPosition = transform.position;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            time = 1.0f;
            spriteRenderer.sprite = stunSprite;
        }
    }

    public void Reset()
    {
        oxygenCount = 0;
    }
}