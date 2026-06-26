using UnityEngine;
using System.Collections.Generic;

public class SubPlayer : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    [SerializeField] float speed = 10; // 酸素の動くスピード

    bool Follow = true;
    bool cooldown = false;
    private float time;

    private float randomx;
    private float randomy;

    public static bool blood_on = true;

    public static List<Transform> SubPlayers = new List<Transform>();//SubPlayerの管理リスト

    public List<Oxygyn> oxygens = new List<Oxygyn>();//Oxygen管理リスト
    public int maxOxygen = 1;//個数調整

    private void Awake()
    {
        SubPlayers.Add(transform);
    }

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;// プレイヤーの座標取得
        SubPlayers.Add(transform);//サブプレイヤー登録

        Vector3 startPos = transform.position;
        startPos.z = -5.0f;
        transform.position = startPos;//初期のZ座標を-5に設定
    }
    private void Update()
    {
        if (time < 0)
        {
            if (blood_on)
            {
                if (Vector2.Distance(transform.position, playerTr.position) < 2f)//プレイヤーとの距離が2f未満の場合
                {
                    Follow = false;
                }
                else
                {
                    Follow = true;
                }

                if (Follow)
                {

                    if (Vector2.Distance(transform.position, playerTr.position) < 0.3f)
                        return;

                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, -5.0f), speed * Time.deltaTime);// プレイヤー追尾

                }
            }
        }

        time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (cooldown == true)
            {
                time = 0;//壁にぶつかると移動を中止
            }
        }

        if (collision.gameObject.tag == "Enemy")
            if (!collision.gameObject.GetComponent<EnemyMove>().IsImpeded)
            {
                time = 1.0f;
            }
    }

    private void OnDestroy()
    {
        SubPlayers.Remove(transform);//リストから削除
    }
}
