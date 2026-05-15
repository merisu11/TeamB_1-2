using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    Vector3 touchWorldPosition;
    public int speed = 5;
    public int Oxygyn_count = 0;
    public int Oxygyn_get = 0;

    // FindGameObjectsWithTag ‚ÌŒÄ‚Ño‚µŠÔŠui•bj
    private float tagCheckInterval = 0.5f;
    private float tagCheckTimer = 0f;

    void Start()
    {
        touchWorldPosition = player.transform.position;
    }

    void Update()
    {
        // “ü—Íˆ—
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchScreenPosition = Input.mousePosition;
            touchScreenPosition.z = 5.0f;
            touchWorldPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition);
        }

        player.transform.position = Vector3.MoveTowards(
            player.transform.position, touchWorldPosition, speed * Time.deltaTime);

        // Ž_‘fƒJƒEƒ“ƒg‚Í–ˆƒtƒŒ[ƒ€‚Å‚Í‚È‚­ˆê’èŠÔŠu‚ÅXV
        tagCheckTimer += Time.deltaTime;
        if (tagCheckTimer >= tagCheckInterval)
        {
            tagCheckTimer = 0f;
            Oxygyn_count = GameObject.FindGameObjectsWithTag("Oxygyn").Length;
            Oxygyn_get = GameObject.FindGameObjectsWithTag("Oxygyn_get").Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            touchWorldPosition = player.transform.position;
        }
    }
}