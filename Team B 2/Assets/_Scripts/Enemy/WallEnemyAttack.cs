using System.Runtime.CompilerServices;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    [SerializeField] private float moveTime = 5f;  // “®‚­ŠÔ
    [SerializeField] private float stopTime = 3f;  // ~‚Ü‚éŠÔ
    [SerializeField] private GameObject wallPrefab;
    private float timer = 0f;
    private bool isMoving = true; // “®‚¢‚Ä‚¢‚é‚©‚Ç‚¤‚©
    private WallEnemyMove moveScript;

    private void Start()
    {
        moveScript = GetComponent<WallEnemyMove>();
        moveScript.canMove = true; // Å‰‚Í“®‚­
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isMoving)
        {
            // moveTime•b“®‚¢‚½‚ç~‚Ü‚é
            if (timer >= moveTime)
            {
                isMoving = false;
                moveScript.canMove = false; // ~‚Ü‚é
                timer = 0f;
            }
        }
        else
        {
            // stopTime•b~‚Ü‚Á‚½‚ç•Ç¶¬‚µ‚Ä“®‚­
            if (timer >= stopTime)
            {
                SpawnWall();
                isMoving = true;
                moveScript.canMove = true; // “®‚«o‚·
                timer = 0f;
            }
        }
    }

    private void SpawnWall()
    {
        Instantiate(wallPrefab, transform.position, Quaternion.identity);
    }
}
