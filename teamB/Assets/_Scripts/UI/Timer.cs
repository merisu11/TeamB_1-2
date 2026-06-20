using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("タイマー設定")]
    [SerializeField] public  static float startTime = 10f;
    [SerializeField] private static float countTime = 3f;
    [Header("UI")]
    [SerializeField] private Text timerText;//制限時間
    [SerializeField] private Text countText;//開始カウントダウン
    private float currentTime;
   
    private bool isRunning = false;
    private bool isCount = false;
    void Start()
    {
        ResetTimer();
        StartTimer();
    }

    private void CountTimer()
    {
        if(!isCount)
        {

        }
    }



    public void StartTimer()
    {
        if (isRunning) return;
        isRunning = true;
        StartCoroutine(CountDown());
    }

    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    public void ResetTimer()
    {
        StopTimer();
        currentTime = startTime;
        UpdateTimerUI();
    }

    public void SetStartTime(float newTime)
    {
        startTime = newTime;
        ResetTimer();
    }

    /// <summary>スキル取得時に呼ぶ。残り時間を増やします。</summary>
    public void AddTime(float seconds)
    {
        currentTime += seconds;
        UpdateTimerUI();
        Debug.Log($"[Timer] +{seconds}秒追加 → 残り {currentTime:F0}秒");
    }

    private IEnumerator CountDown()
    {
        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            if (currentTime < 0f) currentTime = 0f;
            UpdateTimerUI();
        }
        OnTimerEnd();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    private void OnTimerEnd()
    {
        isRunning = false;
        Debug.Log("[Timer] タイムアップ！");

        if (GameManager.Instance != null)
            GameManager.Instance.OnTimerUp();
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}