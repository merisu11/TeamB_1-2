using UnityEngine;

public class GrowingRightAngleLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Points (’¼Špƒ‰ƒCƒ“‚ÌŒo˜H)")]
    [SerializeField] private Vector3 startPoint = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 corner1 = new Vector3(0, 0, 0);     // 1‰ñ–Ú‚Ì’¼Šp
    [SerializeField] private Vector3 corner2 = new Vector3(0, 0, 0);     // 2‰ñ–Ú‚Ì’¼Šp
    [SerializeField] private Vector3 endPoint = new Vector3(0, 0, 0);

    [Header("Config")]
    [SerializeField] private float speed = 15f;   // L‚Ñ‚éƒXƒs[ƒhi’PˆÊ‹——£^•bj

    private float totalLength;
    private float currentLength = 0f;

    private Vector3[] points;
    private float[] segmentLengths;

    private void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer ‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
            enabled = false;
            return;
        }

        // Œo˜H“_‚ğƒZƒbƒg
        points = new Vector3[] { startPoint, corner1, corner2, endPoint };
        lineRenderer.positionCount = points.Length;

        // ŠeƒZƒOƒƒ“ƒg‚Ì’·‚³‚ğŒvZ
        segmentLengths = new float[points.Length - 1];
        totalLength = 0f;

        for (int i = 0; i < points.Length - 1; i++)
        {
            segmentLengths[i] = Vector3.Distance(points[i], points[i + 1]);
            totalLength += segmentLengths[i];
        }

        // ‰Šú‚Í‚·‚×‚Ä startPoint ‚ğ•`‚­
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, startPoint);
        }
    }

    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (speed <= 0f) speed = 0.01f;

        currentLength += speed * Time.deltaTime;
        currentLength = Mathf.Clamp(currentLength, 0f, totalLength);

        float remaining = currentLength;

        for (int i = 0; i < points.Length - 1; i++)
        {
            float segLen = segmentLengths[i];

            if (remaining >= segLen)
            {
                // ‚±‚Ì‹æŠÔ‚Í‘S‚Ä•`‰æÏ‚İ
                lineRenderer.SetPosition(i, points[i]);
                remaining -= segLen;
            }
            else
            {
                // ‚±‚Ì‹æŠÔ‚Ì“r’†‚Ü‚ÅL‚Ñ‚Ä‚¢‚é
                Vector3 pos = Vector3.Lerp(points[i], points[i + 1], remaining / segLen);
                lineRenderer.SetPosition(i, points[i]);
                lineRenderer.SetPosition(i + 1, pos);

                // ˆÈ~‚Í‚Ü‚¾L‚Ñ‚Ä‚¢‚È‚¢‚Ì‚Å“¯‚¶ˆÊ’u‚ÉŒÅ’è
                for (int j = i + 2; j < points.Length; j++)
                {
                    lineRenderer.SetPosition(j, pos);
                }
                break;
            }
        }}
    }
}


