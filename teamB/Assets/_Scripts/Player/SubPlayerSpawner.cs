using UnityEngine;

public class SubPlayerSpawner : MonoBehaviour
{
    public GameObject subPlayerPrefab;

    [Range(0, 9)]
    public int subPlayerCount = 0;

    void Start()
    {
        for (int i = 0; i < subPlayerCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f),
                -5f
            );

            GameObject obj = Instantiate(subPlayerPrefab, pos, Quaternion.identity);
            obj.tag = "SubPlayer";
        }
    }
}