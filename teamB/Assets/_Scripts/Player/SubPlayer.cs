using UnityEngine;

public class SubPlayer : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;
    public static int maxOxygen = 1;

    [SerializeField] float resetTime = 3f;
    private float resetTimer;

    Transform playerTr;
    public float speed = 5f;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        resetTimer = resetTime;
    }

    void Update()
    {
        if (oxygenCount > 0)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0f)
            {
                oxygenCount = 0;
                resetTimer = resetTime;
            }
        }

        if (playerTr == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(playerTr.position.x, playerTr.position.y, transform.position.z),
            speed * Time.deltaTime
        );
    }

    public bool CanGetOxygen()
    {
        return oxygenCount < maxOxygen;
    }

    public void AddOxygen()
    {
        oxygenCount++;
        resetTimer = resetTime;
    }
}