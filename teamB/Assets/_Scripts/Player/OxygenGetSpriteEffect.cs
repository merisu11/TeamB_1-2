using UnityEngine;

/// <summary>
/// 酸素獲得時に表示するスプライトの表示時間管理・自動破棄スクリプト。
/// 指定した表示時間が経過すると、フェードアウトしながら自動でGameObjectを破棄する。
/// Player.PlayOxygenGetEffect() から動的に生成されたGameObjectにAddComponentされる想定。
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class OxygenGetSpriteEffect : MonoBehaviour
{
    [Tooltip("フェードアウトを開始するまでの表示時間（秒）")]
    [SerializeField] private float displayDuration = 0.5f;

    [Tooltip("フェードアウトにかける時間（秒）。0にすると表示時間経過後に即座に消えます")]
    [SerializeField] private float fadeDuration = 0.3f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Player.cs から表示時間・フェード時間を上書き設定する
    public void Setup(float duration, float fade)
    {
        displayDuration = duration;
        fadeDuration = fade;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < displayDuration) return;

        if (fadeDuration <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        float fadeElapsed = timer - displayDuration;
        float alpha = Mathf.Clamp01(1f - fadeElapsed / fadeDuration);
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        if (fadeElapsed >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
