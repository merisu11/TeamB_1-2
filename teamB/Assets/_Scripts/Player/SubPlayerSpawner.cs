using UnityEngine;

public class SubPlayerSpawner : MonoBehaviour
{
    public GameObject subPlayerPrefab;

    [Tooltip("本体（リーダー）のPlayer。ここにシーン上の本物のPlayerをドラッグ&ドロップしてください")]
    public Player leaderPlayer;

    [Range(0, 9)]
    public static int subPlayerCount = 0;

    void Awake()
    {
        Debug.Log($"[SubPlayerSpawner] Awake実行時のsubPlayerCount = {subPlayerCount}");
        // リーダーが未設定なら自動でタグから探す（保険）
        if (leaderPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                leaderPlayer = playerObj.GetComponent<Player>();
            }

            if (leaderPlayer == null)
            {
                Debug.LogError($"[{gameObject.name}] leaderPlayerが設定されておらず、'Player'タグのオブジェクトも見つかりません。SubPlayerを生成できません。");
                return;
            }
        }

        for (int i = 0; i < subPlayerCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(
                 Random.Range(-2f, 2f),
                 Random.Range(-2f, 2f),
                 0f);

            GameObject obj = Instantiate(subPlayerPrefab, pos, Quaternion.identity);
            obj.tag = "SubPlayer";

            // 生成したSubPlayerを「リーダーに追従する側」として設定する
            Player subPlayerScript = obj.GetComponent<Player>();
            if (subPlayerScript != null)
            {
                subPlayerScript.isLeader = false;
                subPlayerScript.leader = leaderPlayer;
                subPlayerScript.slotIndex = i;
                subPlayerScript.slotTotal = subPlayerCount;
            }
            else
            {
                Debug.LogError($"[{gameObject.name}] 生成したprefabにPlayerコンポーネントが見つかりません: {subPlayerPrefab.name}");
            }
        }
    }
}