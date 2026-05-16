using UnityEngine;

public class CameraFit : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        Debug.Log("見つかったRenderer数: " + renderers.Length);

        if (renderers.Length == 0)
        {
            Debug.LogError("Rendererが見つかりません！");
            return;
        }

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            Debug.Log("オブジェクト: " + r.gameObject.name + " / サイズ: " + r.bounds.size);
            bounds.Encapsulate(r.bounds);
        }

        Debug.Log("合計Boundsサイズ: " + bounds.size);
        Debug.Log("合計Bounds中心: " + bounds.center);

        Camera cam = Camera.main;
        float screenAspect = (float)Screen.width / Screen.height;
        float newSize;

        if (screenAspect >= bounds.size.x / bounds.size.y)
            newSize = bounds.size.y / 2f;
        else
            newSize = bounds.size.x / screenAspect / 2f;

        Debug.Log("設定するorthographicSize: " + newSize);
        cam.orthographicSize = newSize;

        cam.transform.position = new Vector3(
            bounds.center.x,
            bounds.center.y,
            cam.transform.position.z
        );
    }
}