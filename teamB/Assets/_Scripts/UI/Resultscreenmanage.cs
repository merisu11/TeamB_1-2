using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//リザルト画面の管理クラスです
//仕様書: 103リザルト画面仕様参照
//GameManagerのデータ・シーン遷移を使用します
public class ResultScreenManager : MonoBehaviour
{

    [SerializeField] private Button retryButton;
    [SerializeField] private Button skillTreeButton;
    [SerializeField] private Text earnedOxygenText;
    //獲得酸素量
    [SerializeField] private Text availableOxygenText;
    //利用可能な酸素

    [SerializeField] private float countDuration = 2.2f;

    private bool isTransitioning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // GameManager.Instance.AddOxygen(50);
        retryButton.onClick.AddListener(OnRetryButtonClicked);
        
        skillTreeButton.onClick.AddListener(OnSkillTreeButtonClicked);

        StartCoroutine(PlayOxygenAnimation());
       
    }

    //リトライボタン(メインゲームに遷移)
    // GameManager.ContinueGame() が OxygenThisRunのリセットとシーンを担当してます
    public void OnRetryButtonClicked()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        retryButton.interactable = false;
        skillTreeButton.interactable = false;

        GameManager.Instance.ContinueGame();
    }

    // スキルツリーボタン
    // 仕様: スキルツリー画面に遷移する
    public void OnSkillTreeButtonClicked()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        retryButton.interactable = false;
        skillTreeButton.interactable = false;

        GameManager.Instance.GoToSkillTree();
    }

    // 酸素カウンターアニメーション
    private IEnumerator PlayOxygenAnimation()
    {
        retryButton.interactable = false;
        skillTreeButton.interactable = false;

        int earned = GameManager.Instance.OxygenThisRun;
        int newTotal = GameManager.Instance.TotalOxygen;
        int prevTotal = newTotal - earned; // アニメーション開始時点の累計

        // 初期表示
        earnedOxygenText.text = earned.ToString("N0");
        availableOxygenText.text = prevTotal.ToString("N0");

        yield return new WaitForSeconds(0.6f);

        // フェーズ1: 獲得酸素量を earned → 0 に減少
        yield return StartCoroutine(CountTo(earnedOxygenText, earned, 0, countDuration));

        yield return new WaitForSeconds(0.2f);

        // フェーズ2: 利用可能な酸素を prevTotal → newTotal に増加
        yield return StartCoroutine(CountTo(availableOxygenText, prevTotal, newTotal, countDuration));

        // アニメーション完了 → ボタン有効化
        retryButton.interactable = true;
        skillTreeButton.interactable = true;
    }

    private IEnumerator CountTo(Text target, int from, int to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = 1f - Mathf.Pow(1f - t, 3f); // EaseOutCubic
            target.text = Mathf.RoundToInt(Mathf.Lerp(from, to, eased)).ToString("N0");
            yield return null;
        }
        target.text = to.ToString("N0");
    }


}
