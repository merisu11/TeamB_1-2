using UnityEngine;

public class gaol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ƒvƒŒƒCƒ„[‚ªŠl“¾‚µ‚½_‘f”‚ğ GameManager ‚É“n‚·
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                GameManager.Instance.AddOxygen(playerScript.Oxygyn_get);
            }

            // ƒS[ƒ‹ˆ— ¨ ƒŠƒUƒ‹ƒg‰æ–Ê‚Ö
            GameManager.Instance.OnGoalReached();
        }
    }
}