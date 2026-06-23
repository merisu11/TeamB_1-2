using UnityEngine;

public class gaol : MonoBehaviour
{
    [SerializeField] private GameObject effectGoalprefab;

    // エフェクトの発生位置（ゴールの奥：画面外側）
    [SerializeField] private Transform effectSpawnPoint;

    // エフェクトが向かう方向（ステージ側：画面内側）
    [SerializeField] private Transform effectTargetPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (effectGoalprefab != null)
            {
                // ゴール奥の位置にエフェクトを生成
                Vector3 spawnPos = effectSpawnPoint != null
                    ? effectSpawnPoint.position
                    : transform.position;

                // ステージ側へ向けて回転
                Vector3 direction = effectTargetPoint != null
                    ? (effectTargetPoint.position - spawnPos).normalized
                    : Vector3.up;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                Instantiate(effectGoalprefab, spawnPos, rotation);
            }

            Player playerScript = other.GetComponent<Player>();
            if (other.gameObject.tag == "Player")
            {
                // プレイヤーが獲得した酸素数を GameManager に渡す
               
                if (playerScript != null)
                {
                    GameManager.Instance.AddOxygen(playerScript.Oxygyn_get);
                }

                Invoke("GoalAfterDelay", 0.5f);//0.5byougo
            }
        }

    }

    private void GoalAfterDelay()
    {
        GameManager.Instance.OnGoalReached();
    }
}