using UnityEngine;

public class SubPlayer : MonoBehaviour, IOxygenTarget
{
    public int oxygenCount = 0;//SubPlayerが持ってる酸素の数
    public static int maxOxygen = 1;//SubPlayerが持てる酸素の数

    [SerializeField] float resetTime = 3f;
    private float resetTimer;

    Transform playerTr;
    public float speed = 5f;
    bool Follow = true;

    private Vector3 touchWorldPosition; // Player不在時（ゴール後）にマウス操作する際の目標位置

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTr = playerObj.transform;
        }
        resetTimer = resetTime;
        touchWorldPosition = transform.position;
    }

    void Update()
    {
        // Playerがゴールしてシーンから消えている場合は、SubPlayer自身をマウスクリックで操作する
        // （Player.csのクリック移動と同じ仕組み）
        if (playerTr == null)
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 5.0f;
                touchWorldPosition = Camera.main.ScreenToWorldPoint(pos);
            }

            transform.position = Vector3.MoveTowards(transform.position, touchWorldPosition, speed * Time.deltaTime);
            return;
        }

        if (Vector2.Distance(transform.position, playerTr.position) < 2f)//プレイヤーとの距離が2f未満の場合
        {
            Follow = false;
        }
        else
        {
            Follow = true;
        }

        if(Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public bool TryGetOxygen()
    {
        if (oxygenCount >= maxOxygen)
        {
            return false;
        }

        oxygenCount++;
        resetTimer = resetTime;
        return true;
    }

    public void Reset()
    {
        oxygenCount = 0;
    }
}