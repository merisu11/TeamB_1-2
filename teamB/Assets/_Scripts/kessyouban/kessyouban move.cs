using UnityEngine;

public class kessyoubanmove : MonoBehaviour
{
    public float cellSize = 0.1f;
    public float wallWidth = 1f;
    public float wallHeight = 2f;
    public Material cellMateria;







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildGrid();
    }

    // Update is called once per frame

    void BuildGrid()
    {
        int cols = Mathf.RoundToInt(wallWidth / cellSize);
        int rows = Mathf.RoundToInt(wallHeight / cellSize);

        for(int x = 0; x < cols; x++)
        for(int y = 0; y < rows; y++)
        {
                float lx = (x + 0.5f) * cellSize - wallWidth * 0.5f;
                float ly = (y + 0.5f) * cellSize - wallHeight * 0.5f;

                GameObject cell = new GameObject($"Cell_{x}_{y}");// x=0, y=0 のとき → "Cell_0_0" という名前のオブジェクトを作る
                cell.transform.SetParent(transform);
                cell.transform.localPosition = new Vector3(lx, ly, 0);
                cell.transform.localScale = Vector3.one * cellSize;
                cell.tag = "EnemyWall";

                var sr = cell.AddComponent<SpriteRenderer>();
                sr.sprite = CreateSquareSprite();
                sr.material = cellMateria;

                cell.AddComponent<BoxCollider2D>();
            }


    }

    public void Carve(Vector2 worldPos, float worldRadius)
    {
        foreach(Transform cell in GetComponentsInChildren<Transform>())
        {
            if (cell == transform) continue;
            float dist = Vector2.Distance(cell.position, worldPos);
            if (dist <= worldRadius)
                Destroy(cell.gameObject);
        }
    }



    // 白い四角スプライトを動的生成
    Sprite CreateSquareSprite()
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
    }


}
