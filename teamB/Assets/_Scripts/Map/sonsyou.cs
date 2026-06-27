using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class sonsyou : MonoBehaviour
{
    [SerializeField] int healRequiredCount = 3;   // 治癒に必要な血小板の数
    [SerializeField] float healTimer = 5.0f;      // 必要数到達後、治癒までの待機時間（秒）
    [SerializeField] Canvas worldCanvas;          // Canvasをアタッチする
    [SerializeField] Image plateletIcon;          // 血小板の画像をアタッチする
    [SerializeField] TextMeshProUGUI countText;   // カウントと秒
    [SerializeField] GameObject healEffectPrefab;      // 回復中（カウントダウン中）に表示するループエフェクト
    [SerializeField] Transform healEffectPoint;        // エフェクトの表示位置（未指定ならこのオブジェクトの位置）
    [SerializeField] float healEffectFadeDelay = 2.0f; // Stop後、エフェクトを破棄するまでの待ち時間（フェードアウト用の余裕）
    private GameObject healEffectInstance;             // 生成したエフェクトの参照（回復終了時に止めるために保持）
    private List<kessyouban> arrivedPlatelets = new List<kessyouban>();
    private List<kessyouban> assignedPlatelets = new List<kessyouban>(); // 向かってきている血小板の一覧
    private bool isHealing = false;   // 治癒カウント中かどうか
    private float currentTimer = 0f; // 現在のカウント秒
    private float baseHealTimer;     // 初期の待機時間（スキル倍率計算の基準値）

    private void Start()
    {
        // 初期値を保存しておく（スキルで何度も呼ばれても元の値から計算できる）
        baseHealTimer = healTimer;
        UpdateUI();
    }

    private void Update()
    {
        if (!isHealing) return;

        currentTimer += Time.deltaTime;
        UpdateUI(); // カウントダウン中は毎フレーム更新
        if (currentTimer >= healTimer)
        {
            HealFloor();
        }
    }

    // ---- 割り当て管理（kessyouban.cs から呼ばれる） ----

    // この損傷に向かう血小板を登録する（kessyouban の Start で呼ぶ）
    public void RegisterAssigned(kessyouban platelet)
    {
        if (!assignedPlatelets.Contains(platelet))
            assignedPlatelets.Add(platelet);
    }

    // 割り当て済みの血小板数を返す
    public int GetAssignedCount()
    {
        // 破棄済みオブジェクトを除いてカウントする
        assignedPlatelets.RemoveAll(p => p == null);
        return assignedPlatelets.Count;
    }

    // 必要数の血小板が既に割り当て済みか（これ以上送り込まなくてよいか）
    public bool IsFullyAssigned()
    {
        return GetAssignedCount() >= healRequiredCount;
    }

    // ---- 到着通知（kessyouban の Update で呼ばれる） ----

    // 血小板が到着したときに kessyouban から呼ばれる
    public void OnPlateletArrived(kessyouban platelet)
    {
        if (!arrivedPlatelets.Contains(platelet))
            arrivedPlatelets.Add(platelet);

        Debug.Log($"[傷口] 血小板到着: {arrivedPlatelets.Count}/{healRequiredCount} isHealing:{isHealing}");
        UpdateUI();
        if (arrivedPlatelets.Count >= healRequiredCount)
        {
            currentTimer = 0f;
            isHealing = true;
            Debug.Log("[傷口] 必要数到達。→ カウントダウン開始");
            UpdateUI(); // 即座にUI切り替え

            SpawnHealEffect(); // 仕様：「回復が開始した瞬間」にエフェクトを表示・生成する
        }
    }

    private void UpdateUI()
    {
        if (countText == null) return;

        if (!isHealing)
        {
            // 到着済みの数だけで「あと何個必要か」を表示する
            // （向かっている途中の血小板はまだ到着していないのでカウントしない）
            int left = healRequiredCount - arrivedPlatelets.Count;
            left = Mathf.Max(0, left);

            if (plateletIcon != null) plateletIcon.gameObject.SetActive(true);
            countText.text = $"\u00D7 {left}";
            Debug.Log($"[傷口] UI更新（待機中）: \u00D7 {left}");
        }
        else
        {
            float left = healTimer - currentTimer;
            left = Mathf.Max(0f, left);

            if (plateletIcon != null)
            {
                plateletIcon.gameObject.SetActive(false);
                countText.text = $"{left:F1}";
                Debug.Log($"[傷口] UI更新（カウントダウン）: {left:F1}");
            }
        }
    }

    private void HealFloor()
    {
        Debug.Log("損傷が回復されました！");

        foreach (var p in arrivedPlatelets)
        {
            if (p != null) Destroy(p.gameObject);
        }

        StopHealEffect(); // 仕様：「回復が終了した瞬間」にエフェクトを止める（フェードアウトしながら消える）

        Destroy(this.gameObject);
    }

    // 回復中ループエフェクトを生成する（回復開始の瞬間に1回だけ呼ぶ）
    private void SpawnHealEffect()
    {
        if (healEffectPrefab == null || healEffectInstance != null) return;

        Vector3 pos = healEffectPoint != null ? healEffectPoint.position : transform.position;
        // 生成してこのオブジェクト(傷)の子にしたい場合は
        // → ただしHealFloor()でこのオブジェクトをDestroyしても、エフェクトはフェードアウトが終わるまで残る
        healEffectInstance = Instantiate(healEffectPrefab, pos, Quaternion.identity);

        // 重ね順の指定（傷 → エフェクト → 血小板の順で手前になるように）に対応
        // 傷（このオブジェクト）のRendererより少し前にエフェクトを置く
        Renderer woundRenderer = GetComponent<Renderer>();
        Renderer effectRenderer = healEffectInstance.GetComponentInChildren<Renderer>();
        if (woundRenderer != null && effectRenderer != null)
        {
            effectRenderer.sortingOrder = woundRenderer.sortingOrder + 1;
        }
    }

    // 回復中ループエフェクトを止める（回復完了の瞬間に1回だけ呼ぶ）
    private void StopHealEffect()
    {
        if (healEffectInstance == null) return;

        ParticleSystem ps = healEffectInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // 新規パーティクルの発生を止める（StopEmitting）
            // すでに出ている粒は寿命まで残るので、エフェクト的に色のフェード設定が活きる
            // そのまま自然にフェードアウトしながら消えてくれる
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        // フェードアウトが完了するくらいの時間を待ってから破棄
        Destroy(healEffectInstance, healEffectFadeDelay);
        healEffectInstance = null;
    }

    // スキルツリー用メソッド

    /// <スキル：回復速度を上げる> 待機時間を倍率で短縮する
    /// スキルツリーから各sonsyouに対して呼び出してください
    public void SetHealSpeedMultiplier(float multiplier)
    {
        // 倍率が大きいほど待機時間が短くなる（早く治癒する）
        // 基準値から計算することで何度呼ばれても元の値になる
        healTimer = baseHealTimer / multiplier;
    }
}