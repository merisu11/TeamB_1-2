using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform playerTr;
    [SerializeField] float speed = 10;
    bool targetplayer = true;
    bool targetEnemy = false;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (targetplayer == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, 1.0f), speed * Time.deltaTime);
                targetEnemy = true;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (targetEnemy == true)
        {
            if (collision.gameObject.CompareTag("hakkekkyuu"))
            {
                targetplayer = false;
                StartCoroutine(DelayCoroutine());

            }
        }
    }
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(2);
        GameObject obj = GameObject.Find("Circle");
        Destroy(obj);
        targetplayer = true;
    }
}
