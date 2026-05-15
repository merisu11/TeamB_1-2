using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Oxygyn : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    [SerializeField] float speed = 10; // 酸素の動くスピード
    bool Follow = false;

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;// プレイヤーの座標取得
    }
    private void Update()
    {
            if (Vector2.Distance(transform.position, playerTr.position) < 1.5f)//プレイヤーとの距離が1.5f未満の場合
            {
                Follow = true;
                this.gameObject.tag = "Oxygyn_count";//取得した酸素のタグを変更する
            }

            if (Follow == true)
            {
                if (Vector2.Distance(transform.position, playerTr.position) < 0.3f)
                    return;

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, -6.0f), speed * Time.deltaTime);// プレイヤー追尾
            }
 
    }   
}
