using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("タイマー設定")]
    [SerializeField] private float startTime = 10f;

    [Header("UI")]
    [SerializeField] private Text timerText;

    private int currentTime;
    private bool isRunning = false;

    void Start()
    {
        ResetTimer();
        StartTimer();
    }

    public void StartTimer()
    {
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
        StopAllCoroutines();
        currentTime = (int)startTime;
        isRunning = false;
        UpdateTimerUI();
    }

    public void SetStartTime(float newTime)
    {
        startTime = newTime;
        ResetTimer();
    }

    private System.Collections.IEnumerator CountDown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // 1秒待つ
            currentTime--;
            UpdateTimerUI();
        }

        OnTimerEnd();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = currentTime.ToString();
    }

    private void OnTimerEnd()
    {
        isRunning = false;
        Debug.Log("タイマー終了！");
    }
}