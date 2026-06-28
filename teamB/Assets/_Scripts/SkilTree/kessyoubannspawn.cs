using UnityEngine;
using System.Collections.Generic;

public class kessyoubannspawn : MonoBehaviour
{
    public GameObject prefab;
    public Transform player;   // プレイヤー
    public static int count = 0;
    public float spawnRadius = 1f; // プレイヤーからの距離

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