using UnityEngine;
using System.Collections.Generic;

public class sonsyou : MonoBehaviour
{
    [SerializeField] int healRequiredCount = 3;
    [SerializeField] float healTimer = 5.0f; 

    private List<kessyouban> arrivedPlatelets = new List<kessyouban>();

    private bool healing = false;
    private float counttimer = 0.0f;
    
    void Update()
    {
        if (!healing) return;

        counttimer += Time.deltaTime;

        if(counttimer >= healTimer )
        {
            HealFloor();
        }
    }


    // 血小板が到着したときに呼ばれる
    public void OnPlateletArrived(kessyouban platelet)
    {
        
        if (!arrivedPlatelets.Contains(platelet))
            arrivedPlatelets.Add(platelet);

        Debug.Log($"血小板到着: {arrivedPlatelets.Count}/{healRequiredCount}");

        if (arrivedPlatelets.Count >= healRequiredCount  )
        {
            healing = true;

        }
    }

    private void HealFloor()
    {

        // 到着した血小板を全て消す
        foreach (var p in arrivedPlatelets)
        {
            if (p != null) Destroy(p.gameObject);
        }

        Destroy(this.gameObject); // 障害物ごと消す
    }
    // スキルツリー用：修復速度の倍率を変更する
    public void SetHealSpeedMultiplier(float multiplier)
    {
        // 倍率が上がるほど必要数が減り修復が早くなる（最低1体は必要）
        healRequiredCount = Mathf.Max(1, Mathf.RoundToInt(healRequiredCount / multiplier));
    }
}