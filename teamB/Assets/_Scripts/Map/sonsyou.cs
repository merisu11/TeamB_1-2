using UnityEngine;

public class OxygenDropFloor : MonoBehaviour
{
    [SerializeField] int dropAmount = 3;

    private bool playerOnFloor = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerOnFloor)
        {
            playerOnFloor = true;
            DropOxygen();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnFloor = false;
        }
    }

    private void DropOxygen()
    {
        // 回収済みの酸素オブジェクトを取得
        GameObject[] collected = GameObject.FindGameObjectsWithTag("Oxygyn_get");
        int actualDrop = Mathf.Min(dropAmount, collected.Length);

        if (actualDrop <= 0) return;

        for (int i = 0; i < actualDrop; i++)
        {
            Oxygyn oxy = collected[i].GetComponent<Oxygyn>();
            if (oxy != null)
            {
                oxy.Release(transform.position); // 解放してその場に落とす
            }
        }
    }
}