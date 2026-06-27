using UnityEngine;

public class SubPlayer : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;//SubPlayer‚ª‚Á‚Ä‚é_‘f‚Ì”
    public static int maxOxygen = 1;//SubPlayer‚ª‚Ä‚é_‘f‚Ì”

    [SerializeField] float resetTime = 3f;
    private float resetTimer;

    Transform playerTr;
    public float speed = 5f;
    bool Follow = true;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        resetTimer = resetTime;
    }

    void Update()
    {
        if (playerTr == null) return;

        if (Vector2.Distance(transform.position, playerTr.position) < 2f)//ƒvƒŒƒCƒ„[‚Æ‚Ì‹——£‚ª2f–¢–‚Ìê‡
        {
            Follow = false;
        }
        else
        {
            Follow = true;
        }

        if(Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public bool TryGetOxygen()
    {
        if (oxygenCount >= maxOxygen)
        {
            return false;
        }

        oxygenCount++;
        resetTimer = resetTime;
        return true;
    }

    public void Reset()
    {
        oxygenCount = 0;
    }
}