using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;   //オブジェクトtag
    Vector3 touchWorldPosition;　//移動先座標の取得
    public int speed = 5;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            touchWorldPosition = player.transform.position;
        }
    }
}

