using UnityEngine;

public class gaol:MonoBehaviour
{ public void OnCollisionEnter2D(Collision2D collision)
    { if (collision.gameObject.tag == "Player")
        {
            Debug.Log("GAMECOLIA");

     }
  }
}
