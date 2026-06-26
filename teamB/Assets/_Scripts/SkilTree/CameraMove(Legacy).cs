using UnityEngine;

public class CameraMove1 : MonoBehaviour
{
    [SerializeField, Tooltip("カメラの移動感度")]
    private float sensitivity = 0.009f;

    private Vector3 touchStartPos;
    private Vector3 cameraStartPos;
    public Vector3 minPos;
    public Vector3 maxPos;
    private bool isDragging = false;

    void Update()
    {
        // クリック/タッチ開始
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            cameraStartPos = transform.position;
            isDragging = true;
        }

        // ドラッグ中
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 difference = Input.mousePosition - touchStartPos;

            // カメラをドラッグと逆方向に移動させる
            Vector3 newPosition = cameraStartPos - new Vector3(difference.x * sensitivity, difference.y * sensitivity, 0);

            // カメラの位置を更新
            newPosition.x = Mathf.Clamp(newPosition.x, minPos.x, maxPos.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPos.y, maxPos.y);


            transform.position = newPosition;

        }

        // クリック/タッチ終了（リリース）
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}

