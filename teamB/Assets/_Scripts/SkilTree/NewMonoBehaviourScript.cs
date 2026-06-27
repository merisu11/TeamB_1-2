using UnityEngine;
using System.Collections.Generic;

public class Prefab_Instantiate : MonoBehaviour
{
    public GameObject prefab;
    public Transform player;   // プレイヤー
    public int count = 5;
    public float spawnRadius = 3f; // プレイヤーからの距離

    List<GameObject> objects = new List<GameObject>();

    void Start()
    {

        for (int i = 0; i < count; i++)
        {
            // 円の中のランダムな位置
            Vector2 offset = Random.insideUnitCircle * spawnRadius;

            Vector3 spawnPos = player.position + new Vector3(offset.x, offset.y, 0);

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            objects.Add(obj);
        }
    }
}