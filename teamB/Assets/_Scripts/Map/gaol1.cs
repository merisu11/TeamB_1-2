using UnityEngine;

public class gaol1 : MonoBehaviour
{
    [SerializeField] private GameObject effectGoalprefab;

    // エフェクトの発生位置（ゴールの画面外側）
    [SerializeField] private Transform effectSpawnPoint;

    // エフェクトが向かう先（ステージ内：画面内側）
    [SerializeField] private Transform effectTargetPoint;

    private int remainingRedCells = -1;   // まだゴールしていない赤血球(Playerタグ)の残り数
    private bool goalCompleted = false;   // ゴール演出（エフェクト・シーン遷移）が済んだか

    private void Start()
    {
        // シーン開始時に存在する「Player」タグ（赤血球）の数を数えておく
        // 仕様：「全ての赤血球がゴールに入った瞬間」エフェクトを出し、その後に遷移
        remainingRedCells = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        if (goalCompleted) return; // すでに全員ゴール済みなら何もしない

        // この赤血球が持っていた酸素を GameManager に加算する（赤血球が運んだ分だけ加点）
        Player playerScript = other.GetComponent<Player>();
        if (playerScript != null)
        {
            GameManager.Instance.AddOxygen(playerScript.Oxygyn_get);
        }

        remainingRedCells--;

        // まだゴールしていない赤血球が残っていればここで終了
        // エフェクト発生とゴール処理（シーン遷移）は「全員そろった」瞬間だけ実行
        if (remainingRedCells > 0) return;

        goalCompleted = true;

        // ===== ここから「全ての赤血球がゴールに入った」「場の処理」 =====
        if (effectGoalprefab != null)
        {
            // ゴール外の位置にエフェクトを生成
            Vector3 spawnPos = effectSpawnPoint != null
                ? effectSpawnPoint.position
                : transform.position;

            // ステージ内へ向けて回転（スマブラの光るエフェクトと同じ、画面外から画面内のイメージ）
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