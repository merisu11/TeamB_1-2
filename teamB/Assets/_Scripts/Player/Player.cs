using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject player;   //オブジェクトtag
    public GameObject[] Oxygyns;
    public GameObject[] Oxygyns_get_1;
    public GameObject[] Oxygyns_get_2;
    Vector3 touchWorldPosition;　//移動先座標の取得
    public int speed = 5;
    public int Oxygyn_count = 0; //場に残ってる酸素の数
    public int Oxygyn_get = 0; //今持っている酸素の数
    public int blood_count = 1;
    private float time;

    void Start()
    {
    }
    void Update()
    {
        if(time < 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 touchScreenPosition = Input.mousePosition;//クリック座標をtouchScreenPositionに
                touchScreenPosition.z = 5.0f;//奥行固定
                Camera camera = Camera.main;
                touchWorldPosition = camera.ScreenToWorldPoint(touchScreenPosition);
            }
            player.transform.position = Vector3.MoveTowards(player.transform.position, touchWorldPosition, speed * Time.deltaTime); //オブジェクトの移動+移動速度
        }
        
        Oxygyns = GameObject.FindGameObjectsWithTag("Oxygyn");//シーン内の酸素の数を数える
        Oxygyn_count = Oxygyns.Length;//Oxygyn_countの数を酸素の数と同一化
        Debug.Log("残ってる酸素の数は" + Oxygyn_count + "個");

        Oxygyns_get_1 = GameObject.FindGameObjectsWithTag("Oxygyn_get.1");//シーン内のメインプレイヤーが獲得した酸素の数を数える
        if (blood_count == 1)
        {
            Oxygyns_get_2 = GameObject.FindGameObjectsWithTag("Oxygyn_get.2");//シーン内のサブプレイヤーが獲得した酸素の数を数える
        }
        Oxygyn_get = Oxygyns_get_1.Length + Oxygyns_get_2.Length;//Oxygyns_getの数を獲得した酸素の数と同一化

        Debug.Log("獲得した酸素の数は" + Oxygyn_get + "個");

        time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            touchWorldPosition = player.transform.position;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            time = 1.0f;
        }
    }
}