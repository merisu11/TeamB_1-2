using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の状態を管理するシングルトン。
/// シーンをまたいで酸素数などのデータを保持します。
/// </summary>
public class GameManager : MonoBehaviour
{
    // ── シングルトン ──────────────────────────────
    public static GameManager Instance { get; private set; }

    // ── 酸素データ ───────────────────────────────
    /// <summary>今回のプレイで獲得した酸素数</summary>
    public int OxygenThisRun { get; private set; } = 0;

    /// <summary>現在所持している酸素数（累計）</summary>
    public int TotalOxygen { get; private set; } = 0;

    // ── ゲーム終了理由 ────────────────────────────
    public enum EndReason { Goal, TimerUp }
    public EndReason LastEndReason { get; private set; }

    // ── シーン名設定 ──────────────────────────────
    [Header("シーン名設定")]
    [SerializeField] private string gameSceneName = "MainGame";
    [SerializeField] private string resultSceneName = "Result";
    [SerializeField] private string skillSceneName = "SkillTree";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ── 酸素の加算 ────────────────────────────────
    public void AddOxygen(int amount)
    {
        OxygenThisRun += amount;
        TotalOxygen += amount;
        Debug.Log($"[GameManager] 酸素追加 +{amount} | 今回: {OxygenThisRun} | 累計: {TotalOxygen}");
    }

    // ── ゲーム終了処理 ────────────────────────────
    /// <summary>プレイヤーがゴールに到達したときに呼ぶ。</summary>
    public void OnGoalReached()
    {
        LastEndReason = EndReason.Goal;
        Debug.Log("[GameManager] ゴール！ → リザルト画面へ");
        LoadResultScene();
    }

    /// <summary>タイマーが 0 になったときに呼ぶ。</summary>
    public void OnTimerUp()
    {
        LastEndReason = EndReason.TimerUp;

        // タイムアップ時は今回の獲得酸素を0に。累計(TotalOxygen)はそのまま。
        TotalOxygen -= OxygenThisRun;
        OxygenThisRun = 0;

        Debug.Log($"[GameManager] タイムアップ！ 今回分リセット | 累計: {TotalOxygen}");
        LoadResultScene();
    }

    // ── シーン遷移 ────────────────────────────────
    private void LoadResultScene()
    {
        SceneManager.LoadScene(resultSceneName);
    }

    /// <summary>「ゲームを続ける」ボタンから呼ぶ。今回の酸素をリセットしてゲームへ。</summary>
    public void ContinueGame()
    {
        OxygenThisRun = 0;
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>「スキルを取る」ボタンから呼ぶ。</summary>
    public void GoToSkillTree()
    {
        OxygenThisRun = 0;
        SceneManager.LoadScene(skillSceneName);
    }
}