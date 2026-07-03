using UnityEngine;

public class SubPlayerSpawner : MonoBehaviour
{
    public GameObject subPlayerPrefab;

    [Range(0, 9)]
    public static int subPlayerCount = 0;

    void Awake()
    {
        for (int i = 0; i < subPlayerCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(
                 Random.Range(-2f, 2f),
                 Random.Range(-2f, 2f),
                 0f);

            GameObject obj = Instantiate(subPlayerPrefab, pos, Quaternion.identity);
            obj.tag = "SubPlayer";
        }
    }
}