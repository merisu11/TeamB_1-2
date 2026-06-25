using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Points (直角ラインの経路)")]
    [SerializeField] private Vector3 startPoint = Vector3.zero;
    [SerializeField] private Vector3 corner1 = Vector3.zero;
    [SerializeField] private Vector3 corner2 = Vector3.zero;
    [SerializeField] private Vector3 endPoint = Vector3.zero;

    [Header("Config")]
    [SerializeField] private float speed = 30f;

    // シーンをまたいで保持されるフラグ
    public static bool lineCompleted = false;

    private float totalLength;
    private float currentLength = 0f;

    private Vector3[] points;
    private float[] segmentLengths;

    // 一度だけ開始するためのフラグ
    private bool started = false;
    private bool canDraw = false;

    private void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer が設定されていません");
            enabled = false;
            return;
        }

        // 経路点をセット
        points = new Vector3[]
        {
            startPoint,
            corner1,
            corner2,
            endPoint
        };

        lineRenderer.positionCount = points.Length;

        // 各区間の長さを計算
        segmentLengths = new float[points.Length - 1];
        totalLength = 0f;

        for (int i = 0; i < points.Length - 1; i++)
        {
            segmentLengths[i] = Vector3.Distance(points[i], points[i + 1]);
            totalLength += segmentLengths[i];
        }

        // すでに描画済みなら最初から完成状態
        if (lineCompleted)
        {
            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }

            currentLength = totalLength;
            started = true;
        }
        else
        {
            // 初期状態は始点のみ表示
            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, startPoint);
            }
        }
    }

    private void Update()
    {
        // ボタンが押されたら一度だけ開始
        if (SkilKM1.ButtonONOFF && !started)
        {
            started = true;
            Invoke(nameof(StartDrawing), 0.5f);
        }

        // 描画中
        if (canDraw && currentLength < totalLength)
        {
            DrawLine();
        }
    }

    private void StartDrawing()
    {
        canDraw = true;
    }

    private void DrawLine()
    {
        currentLength += speed * Time.deltaTime;
        currentLength = Mathf.Clamp(currentLength, 0f, totalLength);

        float remaining = currentLength;

        for (int i = 0; i < points.Length - 1; i++)
        {
            float segLen = segmentLengths[i];

            if (remaining >= segLen)
            {
                lineRenderer.SetPosition(i, points[i]);
                remaining -= segLen;
            }
            else
            {
                Vector3 pos = Vector3.Lerp(
                    points[i],
                    points[i + 1],
                    remaining / segLen
                );

                lineRenderer.SetPosition(i, points[i]);
                lineRenderer.SetPosition(i + 1, pos);

                // 残りは現在位置で固定
                for (int j = i + 2; j < points.Length; j++)
                {
                    lineRenderer.SetPosition(j, pos);
                }

                return;
            }
        }

        // 最後まで描画完了
        lineCompleted = true;
        canDraw = false;

        // 念のため全頂点を正しい位置に設定
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}


