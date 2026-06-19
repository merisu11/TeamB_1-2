using UnityEngine;
using UnityEngine.SceneManagement; //シーン切替に必要

public class tyutoriaru1 : MonoBehaviour
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

            // ゴール処理 → リザルト画面へ
            GameManager.Instance.OnGoalReached();

            //SkillTreeへ移動
            SceneManager.LoadScene("SkillTree");
        }
    }
}
