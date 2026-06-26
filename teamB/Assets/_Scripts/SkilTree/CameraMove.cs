using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField, Tooltip("カメラの移動感度")]
    private float sensitivity = 0.009f;

    [Header("移動範囲")]
    public Vector3 minPos;
    public Vector3 maxPos;

    [Header("ズーム設定")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;

    private Vector3 touchStartPos;
    private Vector3 cameraStartPos;
    private bool isDragging = false;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //==========================
        // ドラッグ移動
        //==========================
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            cameraStartPos = transform.position;
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 difference = Input.mousePosition - touchStartPos;

            Vector3 newPosition = cameraStartPos -
                                  new Vector3(
                                      difference.x * sensitivity,
                                      difference.y * sensitivity,
                                      0);

            newPosition.x = Mathf.Clamp(newPosition.x, minPos.x, maxPos.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPos.y, maxPos.y);

            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        //==========================
        // PC マウスホイールズーム
        //==========================
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize,
                minZoom,
                maxZoom
            );
        }

        //==========================
        // スマホ ピンチズーム
        //==========================
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevDistance = Vector2.Distance(prevTouch0, prevTouch1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            float delta = currentDistance - prevDistance;

            cam.orthographicSize -= delta * 0.01f;
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize,
                minZoom,
                maxZoom
            );
        }
    }
}