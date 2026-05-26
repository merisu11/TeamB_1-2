using UnityEngine;

public class kessyouban : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float rayLength = 1.5f;
    [SerializeField] float healtimer = 5.0f;
    [SerializeField] LayerMask obstacleLayer;

    private Transform targetFloor;
    private bool arrived = false;
    private Vector2 currentDir;
    private Vector2 committedSlide = Vector2.zero; // ★ 決定したスライド方向を記憶

    private void Start()
    {
        GameObject floorObj = GameObject.FindGameObjectWithTag("EnemyWall");
        if (floorObj != null)
        {
            targetFloor = floorObj.transform;
            currentDir = ((Vector2)targetFloor.position - (Vector2)transform.position).normalized;
        }
    }

    private void Update()
    {
        if (arrived || targetFloor == null) return;

        Vector2 pos = (Vector2)transform.position;
        Vector2 toTarget = ((Vector2)targetFloor.position - pos).normalized;

        RaycastHit2D hit = Physics2D.Raycast(pos, toTarget, rayLength, obstacleLayer);

        Vector2 desiredDir;

        if (hit.collider != null && hit.collider.gameObject != targetFloor.gameObject)
        {
            // ★ 壁に当たった最初のフレームだけ方向を決定
            if (committedSlide == Vector2.zero)
            {
                Vector2 surfaceA = new Vector2(-hit.normal.y, hit.normal.x);
                Vector2 surfaceB = new Vector2(hit.normal.y, -hit.normal.x);
                committedSlide = Vector2.Dot(toTarget, surfaceA) > 0 ? surfaceA : surfaceB;
            }

            // ★ スライド方向も塞がれていたら逆方向に切り替え
            RaycastHit2D slideHit = Physics2D.Raycast(pos, committedSlide, rayLength * 0.5f, obstacleLayer);
            if (slideHit.collider != null && slideHit.collider.gameObject != targetFloor.gameObject)
            {
                committedSlide = -committedSlide;
            }

            desiredDir = committedSlide;
        }
        else
        {
            // ★ 道が開いたらリセットして直進
            committedSlide = Vector2.zero;
            desiredDir = toTarget;
        }

        currentDir = Vector2.Lerp(currentDir, desiredDir, Time.deltaTime * 8f);
        if (currentDir.sqrMagnitude < 0.001f) currentDir = toTarget;
        currentDir.Normalize();

        Vector2 move = currentDir * speed * Time.deltaTime;

        // めり込み防止
        RaycastHit2D moveHit = Physics2D.Raycast(pos, move.normalized, move.magnitude + 0.15f, obstacleLayer);
        if (moveHit.collider != null && moveHit.collider.gameObject != targetFloor.gameObject)
        {
            float overlap = Vector2.Dot(move, -moveHit.normal);
            if (overlap > 0)
                move += moveHit.normal * overlap;
        }

        transform.position += (Vector3)move;

        if (Vector2.Distance(transform.position, targetFloor.position) < 1.0f)
        {
            arrived = true;
            targetFloor.GetComponent<sonsyou>()?.OnPlateletArrived(this);
        }
    }
}