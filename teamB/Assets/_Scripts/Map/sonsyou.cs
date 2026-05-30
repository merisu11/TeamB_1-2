using UnityEngine;
using System.Collections.Generic;

public class sonsyou : MonoBehaviour
{
    [SerializeField] int healRequiredCount = 3;   // 修復に必要な血小板の数
    [SerializeField] float healTimer = 5.0f;      // 必要数到着後、修復までの待機時間（秒）

    private List<kessyouban> arrivedPlatelets = new List<kessyouban>();
    private bool isHealing = false;   // 修復カウント中かどうか
    private float currentTimer = 0f; // 現在のカウント時間
    private float baseHealTimer;     // 初期の待機時間（スキル倍率計算の基準値）

    private void Start()
    {
        // 初期値を保存しておく（スキルで何度呼ばれても元の値を基準にできる）
        baseHealTimer = healTimer;
    }

    private void Update()
    {
        if (!isHealing) return;

        currentTimer += Time.deltaTime;

        if (currentTimer >= healTimer)
        {
            HealFloor();
        }
    }

    // 血小板が到着したときにkessyouban側から呼ばれる
    public void OnPlateletArrived(kessyouban platelet)
    {
        if (!arrivedPlatelets.Contains(platelet))
            arrivedPlatelets.Add(platelet);

        Debug.Log($"血小板到着: {arrivedPlatelets.Count}/{healRequiredCount}");

        if (arrivedPlatelets.Count >= healRequiredCount)
        {
            isHealing = true;
            Debug.Log($"修復開始：{healTimer}秒後に修復されます");
        }
    }

    private void HealFloor()
    {
        Debug.Log("障害物が除去されました！");

        foreach (var p in arrivedPlatelets)
        {
            if (p != null) Destroy(p.gameObject);
        }

        Destroy(this.gameObject);
    }

    //  スキルツリー用メソッド

    
    /// 【スキル：治癒速度を上げる】待機時間を倍率で短縮する
    /// スキルツリー側から全sonsyouに対して呼び出してください
   
    public void SetHealSpeedMultiplier(float multiplier)
    {
        // 倍率が大きいほど待機時間が短くなる（速く修復される）
        // 基準値から計算することで何度呼ばれても正しい値になる
        healTimer = baseHealTimer / multiplier;
    }
}