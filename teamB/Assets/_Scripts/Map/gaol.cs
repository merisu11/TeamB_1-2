using UnityEngine;

public class gaol : MonoBehaviour
{
    [SerializeField] private GameObject effectGoalprefab;
    [SerializeField] private Transform effectSpawnPoint;
    [SerializeField] private Transform effectTargetPoint;

    private int remainingRedCells = -1;
    private bool goalCompleted = false;

    private void Start()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        int subPlayerCount = GameObject.FindGameObjectsWithTag("SubPlayer").Length;
        remainingRedCells = playerCount + subPlayerCount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayerTag = other.gameObject.CompareTag("Player");
        bool isSubPlayerTag = other.gameObject.CompareTag("SubPlayer");
        if (!isPlayerTag && !isSubPlayerTag) return;
        if (goalCompleted) return;

        // Player・SubPlayerどちらも今はPlayerコンポーネントを使っているので統一して取得する
        Player reachedPlayer = other.GetComponent<Player>();
        if (reachedPlayer != null)
        {
            GameManager.Instance.AddOxygen(reachedPlayer.oxygenCount);

            // ゴールしたのが操作対象（リーダー）だった場合、
            // 残っている個体の中から次のリーダーを選出して操作を引き継ぐ
            if (reachedPlayer.isLeader)
            {
                PromoteNewLeader(reachedPlayer);
            }
        }

        remainingRedCells--;
        Destroy(other.gameObject);

        if (remainingRedCells > 0) return;

        goalCompleted = true;

        if (effectGoalprefab != null)
        {
            Vector3 spawnPos = effectSpawnPoint != null ? effectSpawnPoint.position : transform.position;
            Vector3 direction = effectTargetPoint != null ? (effectTargetPoint.position - spawnPos).normalized : Vector3.up;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(effectGoalprefab, spawnPos, rotation);
        }

        Invoke("GoalAfterDelay", 0.5f);
    }

    // 現在のリーダーがゴールして消える前に、残りの中から新しいリーダーを選ぶ
    private void PromoteNewLeader(Player oldLeader)
    {
        Player[] allPlayers = FindObjectsByType<Player>(FindObjectsSortMode.None);

        Player newLeader = null;
        foreach (Player p in allPlayers)
        {
            if (p == oldLeader) continue;
            newLeader = p;
            break;
        }

        // 誰も残っていない（これが最後の1体だった）場合は何もしない
        if (newLeader == null) return;

        newLeader.isLeader = true;
        newLeader.leader = null;

        // 残り全員のleader参照を新リーダーに更新する
        foreach (Player p in allPlayers)
        {
            if (p == newLeader) continue;
            p.isLeader = false;
            p.leader = newLeader;
        }
    }

    private void GoalAfterDelay()
    {
        GameManager.Instance.OnGoalReached();
    }
}