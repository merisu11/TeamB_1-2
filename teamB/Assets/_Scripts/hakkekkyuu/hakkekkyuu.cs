using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hakkekkyuu : MonoBehaviour
{
    Transform playerTr;
    Transform enemyTy;
    [SerializeField] float speed = 10;
    bool targetplayer = true;
    bool targetEnemy = false;
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTy = GameObject.FindGameObjectWithTag("Enemy").transform;

    }
    private void Update()
    {
        if (targetplayer == true)
        {
              transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, 1.0f), speed * Time.deltaTime);
              targetEnemy = true;
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(targetplayer == true)
        {
            if (collision.gameObject.CompareTag("Untagged"))
            {
                speed = 0;
            }
        }
        if (targetEnemy == true)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyTy.position.x, enemyTy.position.y, 1.0f), speed * Time.deltaTime);
                targetplayer = false;
            }
        }
    }
}
