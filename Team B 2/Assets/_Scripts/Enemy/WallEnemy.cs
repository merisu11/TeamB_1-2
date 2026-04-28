using UnityEngine;
using UnityEngine.UIElements;

public class WallEnemyMove:MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float changeTime = 2f; //方向転換
    private Vector2 moveDirection;
    private float timer;

    private void Start()
    {
        ChangeDirection();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= changeTime)
        {
            ChangeDirection();
            timer = 0f;
        }
        transform.Translate(moveDirection * speed * Time.deltaTime);//方向×速さ
        // Translate → 今の場所から相対的に移動
        //今の場所から少しずつ動かす
    }

    private void ChangeDirection()
    {
        float x = Random.Range(-1f, 1f);//(最小値,最大値)の中でランダムに数字を入れる
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y).normalized;//斜めでも1に統一される
    }
}
