using UnityEngine;
using UnityEngine.SceneManagement; //シーン切替に必要

public class tyutoriarugaol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // プレイヤーが獲得した酸素数を GameManager に渡す
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                GameManager.Instance.AddOxygen(playerScript.oxygenCount);
            }



            //SkillTreeへ移動
            GameManager.Instance.GoToSkillTree();
        }
    }
}
