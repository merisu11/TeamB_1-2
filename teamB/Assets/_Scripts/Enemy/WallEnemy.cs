using UnityEngine;

public class WallEnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float changeTime = 2f;
    private Vector2 moveDirection;
    private float timer;
    public bool canMove = true;

    private void Start()
    {
        ChangeDirection();
    }

    private void Update()
    {
        if (!canMove) return;

        timer += Time.deltaTime;
        if (timer >= changeTime)
        {
            ChangeDirection();
            timer = 0f;
        }

        // i‚Ş•ûŒü‚É•Ç‚ª‚ ‚é‚©’²‚×‚é
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            moveDirection,
            speed * Time.deltaTime + 0.1f
        );

        // •Ç‚É“–‚½‚è‚»‚¤‚È‚ç•ûŒü“]Š·
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            moveDirection = Vector2.Reflect(moveDirection, hit.normal);
            timer = 0f;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void ChangeDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y).normalized;
    }
}