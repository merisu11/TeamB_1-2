using UnityEngine;
using System.Collections.Generic;

public class Prefab_Instantiate : MonoBehaviour
{
    public GameObject prefab;
    public int count = 5;

    List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        SubPlayer.blood_on = true;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(
                prefab,
                new Vector3(i * 2, 0, 0),
                Quaternion.identity
            );

            objects.Add(obj);
        }
    }
}