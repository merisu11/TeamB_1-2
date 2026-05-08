using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]//é©ìÆÇ≈í«â¡
[RequireComponent(typeof(CircleCollider2D))]
public class destroy : MonoBehaviour
{
    [Header("åüímÅEà⁄ìÆ")]
    public float detectRange = 5f;
    public float seekSpeed = 2f;
    public float chargeSpeed = 8f;

    [Header("îjâÛ")]
    public float carveRatio = 0.5f;

    private Rigidbody2D rb;
    private CircleCollider2D col;
    private bool isCharging;
    private Transform chargeTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.linearDamping = 0f; // ñÄéCÉ[Éç
        rb.angularDamping = 0f;
        rb.gravityScale = 0f; // èdóÕÉ[Éç

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.material = new Material(Shader.Find("Sprites/Default"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            if (chargeTarget != null)
            {
                Vector2 dir = (chargeTarget.position - transform.position).normalized;
                rb.linearVelocity = dir * chargeSpeed;
            }
            return;
        }

        List<Collider2D> hits = new List<Collider2D>();
        Physics2D.OverlapCircle(
     transform.position,
     detectRange,
     ContactFilter2D.noFilter,
     hits
 );

        Transform nearest = FindNearest(hits);

        if(nearest != null)
        {
            Vector2 dir = (nearest.position - transform.position).normalized;
            rb.linearVelocity = dir * chargeSpeed;
            isCharging        = true;



        }
        else
        {
            rb.linearVelocity = Vector2.right * seekSpeed;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"ìñÇΩÇ¡ÇΩ: {other.gameObject.name} É^ÉO: {other.tag}");

        if (!other.CompareTag("EnemyWall"))
        {
            Debug.Log("EnemyWallÉ^ÉOÇ≈ÇÕÇ»Ç¢ÇÃÇ≈return");
            return;
        }


     

        float worldRadius = col.radius * transform.lossyScale.x;
        float carveRadius = worldRadius * carveRatio;

        other.GetComponentInParent<kessyoubanmove>()?.Carve(transform.position, carveRadius);

        Destroy(gameObject);


    }







    Transform FindNearest(List<Collider2D> hits)
    {
        Transform best = null;
        float minD = float.MaxValue;
        foreach (var h in hits)
        {
            if (!h.CompareTag("EnemyWall")) continue;
            float d = Vector2.Distance(transform.position, h.transform.position);
            if (d < minD) { minD = d; best = h.transform; }
        }
        return best;
    }
}
