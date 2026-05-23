using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;   //オブジェクトtag
    public GameObject[] Oxygyns;
    public GameObject[] Oxygyns_get;
    Vector3 touchWorldPosition;　//移動先座標の取得
    public int speed = 5;
    public int Oxygyn_count = 0; //場に残ってる酸素の数
    public int Oxygyn_get = 0; //今持っている酸素の数

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchScreenPosition = Input.mousePosition;//クリック座標をtouchScreenPositionに
            touchScreenPosition.z = 5.0f;//奥行固定
            Camera camera = Camera.main;
            touchWorldPosition = camera.ScreenToWorldPoint(touchScreenPosition);
        }
        player.transform.position = Vector3.MoveTowards(player.transform.position, touchWorldPosition, speed * Time.deltaTime); //オブジェクトの移動+移動速度

        Oxygyns = GameObject.FindGameObjectsWithTag("Oxygyn");//シーン内の酸素の数を数える
        Oxygyn_count = Oxygyns.Length;//Oxygyn_countの数を酸素の数と同一化
        Debug.Log("残ってる酸素の数は" + Oxygyn_count + "個");

        Oxygyns_get = GameObject.FindGameObjectsWithTag("Oxygyn_get.1");//シーン内の獲得した酸素の数を数える
        Oxygyn_get = Oxygyns_get.Length;//Oxygyns_getの数を獲得した酸素の数と同一化
        Debug.Log("獲得した酸素の数は" + Oxygyn_get + "個");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            touchWorldPosition = player.transform.position;
        }
    }
}

