using UnityEngine;

public class OxygenSpawner : MonoBehaviour
{
    [Header("酸素のPrefab")]
    [SerializeField] private GameObject oxygenPrefab;

    [Header("スポーン設定")]
    [SerializeField] private int spawnCount = 30;

    [Header("スポーン範囲（ステージの広さに合わせて調整）")]
    [SerializeField] private float rangeX = 8f;
    [SerializeField] private float rangeY = 4f;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(-rangeX, rangeX);
            float y = Random.Range(-rangeY, rangeY);
            Instantiate(oxygenPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
    }
}