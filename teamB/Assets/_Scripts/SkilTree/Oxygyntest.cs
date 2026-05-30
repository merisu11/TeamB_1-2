using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class Oxygyntest : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    Transform subplayerTr; // プレイヤーのTransform
    [SerializeField] public static float speed = 10; // 酸素の動くスピード
    public GameObject[] Oxygyns_1;
    public GameObject[] Oxygyns_2;
    bool Follow = false;
    bool subFollow = false;
    bool cooldown = false;
    private float time;
    private float randomx;
    private float randomy;
    public int Oxygyn1;
    public int Oxygen2;
    public static int Max_oxygyns = 1;
    //public int Max_oxygyn1 = 1;
    //public int Max_oxygyn2 = 1;

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;// プレイヤーの座標取得
        subplayerTr = GameObject.FindGameObjectWithTag("SubPlayer").transform;// サブプレイヤーの座標取得
        Vector3 startPos = transform.position;
        startPos.z = -6.0f;
        transform.position = startPos;//初期のZ座標を-6に設定
    }
    private void Update()
    {

        Oxygyns_1 = GameObject.FindGameObjectsWithTag("Oxygyn_get.1");
        Oxygyns_2 = GameObject.FindGameObjectsWithTag("Oxygyn_get.2");
        Oxygyn1 = Oxygyns_1.Length;
        Oxygen2 = Oxygyns_2.Length;

        if (Vector2.Distance(transform.position, playerTr.position) < 1.5f)//プレイヤーとの距離が1.5f未満の場合
        {
            if (cooldown == false)
            {
                if (Oxygyn1 < Max_oxygyns)
                {
                    Follow = true;
                    subFollow = !Follow;
                    this.gameObject.tag = "Oxygyn_get.1";//取得した酸素のタグを変更
                }
            }        
        }

        if (Vector2.Distance(transform.position, subplayerTr.position) < 1.5f)//サブプレイヤーとの距離が1.5f未満の場合
        {
            if (cooldown == false)
            {
                if (Oxygen2 < Max_oxygyns)
                {
                    subFollow = true;
                    Follow = !subFollow;
                    this.gameObject.tag = "Oxygyn_get.2";//取得した酸素のタグを変更
                }
            }
        }

        if (Follow == true)
        {
            
            if (Vector2.Distance(transform.position, playerTr.position) < 0.3f)
                return;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, -6.0f), speed * Time.deltaTime);// プレイヤー追尾
                   
        }

        if (subFollow == true)
        {

            if (Vector2.Distance(transform.position, subplayerTr.position) < 0.3f)
                return;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(subplayerTr.position.x, subplayerTr.position.y, -6.0f), speed * Time.deltaTime);// プレイヤー追尾

        }

        time -= Time.deltaTime;

        if (cooldown == true)//病原菌と衝突した後の処理
        {
            Follow = false;
            subFollow = false;
            this.gameObject.tag = "Oxygyn";
            if (time >= 0.8f)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomx, randomy), 10.0f * Time.deltaTime);//ランダムに決定した座標に移動
            }
        }

        if (time <= 0)
        {
            cooldown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Follow == true)
            {
                cooldown = true;
                time = 1.0f;
                if (cooldown == true)
                {
                    randomx = Random.Range(-9.0f, 9.0f);//9～-9の値からランダムに決定
                    randomy = Random.Range(-4.5f, 4.5f);//4.5～-4.5の値からランダムに決定
                }
            }

            if (subFollow == true)
            {
                cooldown = true;
                time = 1.0f;
                if (cooldown == true)
                {
                    randomx = Random.Range(-9.0f, 9.0f);//9～-9の値からランダムに決定
                    randomy = Random.Range(-4.5f, 4.5f);//4.5～-4.5の値からランダムに決定
                }
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if(cooldown == true)
            {
                time = 0;//壁にぶつかると移動を中止
            }
        }
    }
}
