using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [Header("タイマー設定")]
    [SerializeField] private float startTime = 10f;

    [Header("UI")]
    [SerializeField] private Text timerText;

    private int currentTime;
   

    void Start()
    {
        ResetTimer();
        StartTimer();
    }

    public void StartTimer()
    {
       
        StartCoroutine(CountDown());
    }

    public void StopTimer()
    {
       
        StopAllCoroutines();
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        currentTime = (int)startTime;
        
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
        
        Debug.Log("タイマー終了！");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}