using UnityEngine;

public class Player : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;
    public static int maxOxygen = 1;

    [SerializeField] float resetTime = 3f;
    private float resetTimer;

    public static int speed = 5;
    Vector3 touchWorldPosition;
    private float time;

    void Start()
    {
        touchWorldPosition = transform.position;
        resetTimer = resetTime;
    }

    void Update()
    {
        // =========================
        // 自動リセット（重要）
        // =========================
        if (oxygenCount > 0)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0f)
            {
                oxygenCount = 0;
                resetTimer = resetTime;
            }
        }

        if (time < 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 5.0f;

                touchWorldPosition = Camera.main.ScreenToWorldPoint(pos);
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                touchWorldPosition,
                speed * Time.deltaTime
            );
        }

        time -= Time.deltaTime;
    }

    public bool CanGetOxygen()
    {
        return oxygenCount < maxOxygen;
    }

    public void AddOxygen()
    {
        oxygenCount++;
        resetTimer = resetTime; // ★取得したらリセット延長
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchWorldPosition = transform.position;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            time = 1.0f;
        }
    }
}