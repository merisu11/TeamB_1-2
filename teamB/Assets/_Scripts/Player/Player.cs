using UnityEngine;

public class Player : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;//Player‚ª‚Á‚Ä‚é_‘f‚Ì”
    public static int maxOxygen = 1;//Player‚ª‚Ä‚é_‘f‚Ì”

    [SerializeField] float resetTime = 3f;

    public static int speed = 5;
    Vector3 touchWorldPosition;
    private float time;

    void Start()
    {
        touchWorldPosition = transform.position;
    }

    void Update()
    {

        if (time < 0)
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 5.0f;

                touchWorldPosition = Camera.main.ScreenToWorldPoint(pos);
            }

            transform.position = Vector3.MoveTowards(transform.position,touchWorldPosition,speed * Time.deltaTime);
        }

        time -= Time.deltaTime;
    }

    public bool TryGetOxygen()
    {

        if (oxygenCount >= maxOxygen)
        {
            return false;
        }

        oxygenCount++;

        return true;
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

    public void Reset()
    {
        oxygenCount = 0;
    }
}