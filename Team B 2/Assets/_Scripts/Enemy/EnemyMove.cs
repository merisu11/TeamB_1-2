using UnityEngine;

public class EnemyMove : MonoBehaviour  //MonoBehaviourを引き継ぐことで
//Unityのオブジェクトにアタッチできるようになる
{
    [SerializeField] private float speed = 3f;// [SerializeField] privateだけどUnityから値を買えれるように
    private Transform player;//これにPlayerの位置情報を入れる
    private void Start()//最初に一回だけ
    {
        player = GameObject.FindWithTag("Player").transform;
        //Playerタグが付いてるオブジェクトを探す
        //transformでそのオブジェクトの位置情報を取得
    }

    private void Update()//tuneni
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // AからBに向かってCの速さで移動する命令
        //現在位置情報,プレイヤーの位置情報,速さ　を位置情報にまいふれーむ入れる
        

    }

}
