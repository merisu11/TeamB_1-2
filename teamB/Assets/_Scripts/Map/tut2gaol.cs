using UnityEngine;

public class tut2gaol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // プレイヤーが獲得した酸素数を GameManager に渡す
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                GameManager.Instance.AddOxygen(playerScript.Oxygyn_get);
            }

            // ゴール処理 → スキルツリー画面へ
            GameManager.Instance.GoToSkillTree();
        }
    }
}