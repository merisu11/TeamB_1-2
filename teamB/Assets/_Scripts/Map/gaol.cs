using UnityEngine;

public class gaol : MonoBehaviour
{
    [SerializeField] private GameObject effectGoalprefab;

    // エフェクトの発生位置（ゴールの奥：画面外側）
    [SerializeField] private Transform effectSpawnPoint;

    // エフェクトが向かう方向（ステージ側：画面内側）
    [SerializeField] private Transform effectTargetPoint;

    private int remainingRedCells = -1;   // まだゴールしていない赤血球(Player/SubPlayerタグ)の残数
    private bool goalCompleted = false;   // ゴール演出（エフェクト・シーン進行）が済んだか

    private void Start()
    {
        // シーン開始時に存在する「Player」「SubPlayer」タグ（赤血球）の総数を数えておく
        // 仕様：「全ての赤血球がゴールに入ったら」エフェクトを出す、の判定基準にする
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        int subPlayerCount = GameObject.FindGameObjectsWithTag("SubPlayer").Length;
        remainingRedCells = playerCount + subPlayerCount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player か SubPlayer のどちらかのタグでなければ無視する
        bool isPlayerTag = other.gameObject.tag == "Player";
        bool isSubPlayerTag = other.gameObject.tag == "SubPlayer";
        if (!isPlayerTag && !isSubPlayerTag) return;
        if (goalCompleted) return; // 既に全員ゴール済みなら何もしない

        // この赤血球が獲得した酸素数を GameManager に加算する（赤血球が来るたび毎回行う）
        // Player・SubPlayer どちらも oxygenCount フィールドを持っているのでそれを見る
        if (isPlayerTag)
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                GameManager.Instance.AddOxygen(playerScript.oxygenCount);
            }
        }
        else // isSubPlayerTag
        {
            SubPlayer subPlayerScript = other.GetComponent<SubPlayer>();
            if (subPlayerScript != null)
            {
                GameManager.Instance.AddOxygen(subPlayerScript.oxygenCount);
            }
        }

        remainingRedCells--;

        // ゴールした個体は画面から消す
        // （Mainタグの「Player」は操作対象なので、本当に消してよいか要確認。
        //   ひとまず仕様通りに両方Destroyする実装にしています）
        Destroy(other.gameObject);

        // まだゴールしていない赤血球が残っていればここで終了
        // エフェクト発生とゴール処理（シーン進行）は「全員到着」の瞬間だけ行う
        if (remainingRedCells > 0) return;

        goalCompleted = true;

        // ===== ここから「全ての赤血球がゴールに入った」瞬間の処理 =====
        if (effectGoalprefab != null)
        {
            // ゴール奥の位置にエフェクトを生成
            Vector3 spawnPos = effectSpawnPoint != null
                ? effectSpawnPoint.position
                : transform.position;

            // ステージ側へ向けて回転（スマブラの撃墜エフェクトと同じ、画面外→画面内のイメージ）
            Vector3 direction = effectTargetPoint != null
                ? (effectTargetPoint.position - spawnPos).normalized
                : Vector3.up;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(effectGoalprefab, spawnPos, rotation);
        }

        Invoke("GoalAfterDelay", 0.5f); // 0.5秒後
    }

    private void GoalAfterDelay()
    {
        GameManager.Instance.OnGoalReached();
    }
}
