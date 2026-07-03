using UnityEngine;
using System.Collections; //コールチンを使うため
using UnityEngine.UI;


public class ClearSceneManager : MonoBehaviour
{
    [Header("3人の移動演出設定")]
    public GameObject performers; //3人とパネルが入った親オブジェクト（ClearPerforme
    public Transform targetPosition; //画面真ん中上の目標値（TargetPosition)
    public float speed = 10000.0f;   //運んでくるスピード

    [Header("6秒後に出したい2つのボタン")]
    public GameObject titleButton;
    public GameObject mainGameButton;

    [Header("酸素量カウンターのUI")]
    [SerializeField] private Text earnedOxygenText; //獲得酸素量テキスト
    [SerializeField] private Text availableOxygenText; //利用可能ナ酸素テキスト
    [SerializeField] private float countDuration = 2.2f;

    private bool isArrived = false;
    private bool isAnimating;
    private bool skipRequested;
    private bool oxygenAnimationDone = false; // 酸素(リザルト)演出が完了したか

    void Start()
    {
        //最初は2つのボタンを両方友非表示にしておく
        if (titleButton != null) titleButton.SetActive(false);
        if (mainGameButton != null) mainGameButton.SetActive(false);

        // シーンが切り替わった瞬間、念のため位置を初期化（スタート位置にある状態）

        // ゲーム開始時点（移動中）は、酸素テキストを仮で表示
        if (GameManager.Instance != null)
        {
            int earned = GameManager.Instance.OxygenThisRun;
            int newTotal = GameManager.Instance.TotalOxygen;
            int prevTotal = newTotal - earned;

            if (earnedOxygenText != null) earnedOxygenText.text = earned.ToString("N0");
            if (availableOxygenText != null) availableOxygenText.text = prevTotal.ToString("N0");
        }

        // ボタンを押した時の遷移処理を登録
        if (titleButton != null)
        {
            Button btn = titleButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(OnTitleButtonClicked);
        }
        if (mainGameButton != null)
        {
            Button btn = mainGameButton.GetComponent<Button>();
            if (btn != null) btn.onClick.AddListener(OnMainGameButtonClicked);
        }

        // 文字の移動演出(Updateで開始)と同時進行でリザルト(酸素)処理も開始する
        StartCoroutine(PlayOxygenAnimation());

        // 移動演出とリザルト処理の両方が終わってからボタン表示のタイマーを始める
        StartCoroutine(WaitForBothThenShowButtons());
    }
    private void Update()
    {
        //まだ目標地点についていないなら、毎フレーム真ん中に向かって移動させる
        if (!isArrived && performers != null && targetPosition != null)
        {
            MoveToTarget();
        }

        // 酸素カウント中にクリックされたらスキップ
        if (isAnimating && Input.GetMouseButtonDown(0))
        {
            skipRequested = true;
        }
    }
    void MoveToTarget()
    {
        //3人の塊（performers)を目標地点へスムーズに移動
        performers.transform.position = Vector3.MoveTowards(
           performers.transform.position,
           targetPosition.position,
           speed * Time.deltaTime
           );
        //画面真ん中にピッタリ(誤差0.05歩以内)に着いたら停止
        if (Vector3.Distance(performers.transform.position, targetPosition.position) < 0.05f)
        {
            isArrived = true;
            Debug.Log("画面中央に到着しました");
        }
    }

    // 酸素カウンターアニメーション(文字の移動演出と同時に実行される)
    private IEnumerator PlayOxygenAnimation()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManagerが見つかりません。");
            oxygenAnimationDone = true;
            yield break;
        }

        isAnimating = true;

        int earned = GameManager.Instance.OxygenThisRun;
        int newTotal = GameManager.Instance.TotalOxygen;
        int prevTotal = newTotal - earned;

        yield return new WaitForSeconds(0.6f);

        // フェーズ1: 獲得酸素量を earned → 0 に減少
        yield return StartCoroutine(CountTo(earnedOxygenText, earned, 0, countDuration));

        // フェーズ1中にスキップされていたら終了（フェーズ2を実行しない）
        if (!isAnimating)
        {
            oxygenAnimationDone = true;
            yield break;
        }

        yield return new WaitForSeconds(0.2f);

        // フェーズ2: 利用可能な酸素を prevTotal → newTotal に増加
        yield return StartCoroutine(CountTo(availableOxygenText, prevTotal, newTotal, countDuration));

        isAnimating = false;

        // 🌟酸素演出が完了(移動演出と合わせてWaitForBothThenShowButtonsが検知する)
        oxygenAnimationDone = true;
    }

    private IEnumerator CountTo(Text target, int from, int to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (skipRequested)
            {
                // 両テキストを最終値に一気に設定して終了
                if (earnedOxygenText != null) earnedOxygenText.text = 0.ToString("N0");
                if (availableOxygenText != null) availableOxygenText.text = GameManager.Instance.TotalOxygen.ToString("N0");

                skipRequested = false;
                isAnimating = false;
                oxygenAnimationDone = true;
                yield break;
            }
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = 1f - Mathf.Pow(1f - t, 3f); // EaseOutCubic
            if (target != null) target.text = Mathf.RoundToInt(Mathf.Lerp(from, to, eased)).ToString("N0");
            yield return null;
        }
        if (target != null) target.text = to.ToString("N0");
    }

    // 移動演出とリザルト(酸素)処理の両方が終わるのを待ってから6秒後にボタンを出す
    private IEnumerator WaitForBothThenShowButtons()
    {
        yield return new WaitUntil(() => isArrived && oxygenAnimationDone);

        yield return new WaitForSeconds(3.0f);

        //両方終わって6秒たったら2つのボタンを出す
        if (titleButton != null) titleButton.SetActive(true);
        if (mainGameButton != null) mainGameButton.SetActive(true);
    }

    // 「タイトルに戻る」ボタン → タイトル画面へ
    private void OnTitleButtonClicked()
    {
        if (GameManager.Instance != null) GameManager.Instance.GoToTitleScene();
    }

    // 「ゲームを続ける」ボタン → メインゲームへ
    private void OnMainGameButtonClicked()
    {
        if (GameManager.Instance != null) GameManager.Instance.ContinueGame();
    }
}