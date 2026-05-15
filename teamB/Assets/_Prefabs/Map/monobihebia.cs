using UnityEngine;

public class monobihebia : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
     public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("GAMECOLIA");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
