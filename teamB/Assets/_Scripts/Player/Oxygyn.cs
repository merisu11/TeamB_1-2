using UnityEngine;

public class Oxygyn : MonoBehaviour
{
    Transform playerTr;
    [SerializeField] float speed = 10;
    bool Follow = false;

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 1.5f)
        {
            Follow = true;
            this.gameObject.tag = "Oxygyn_get";
        }

        if (Follow == true)
        {
            if (Vector2.Distance(transform.position, playerTr.position) < 0.3f)
                return;

            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(playerTr.position.x, playerTr.position.y, -6.0f),
                speed * Time.deltaTime);
        }
    }
}