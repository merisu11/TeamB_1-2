using UnityEngine;

// 血小板クラス
// 損傷箇所（EnemyWallタグのオブジェクト）に向かって移動し、到着したらsonsyouに通知する
public class kessyouban : MonoBehaviour
{
    [SerializeField] float speed = 3f;          // 移動速度
    [SerializeField] float rayLength = 13.0f;    // 障害物検知レイの長さ
    [SerializeField] LayerMask obstacleLayer;   // 障害物と判定するレイヤー

    private Transform targetFloor;              // 向かう目標（損傷箇所）
    private bool arrived = false;               // 到着済みかどうか
    private Vector2 currentDir;                 // 現在の移動方向（なめらかに変化させるために保持）
    private Vector2 committedSlide = Vector2.zero; // 障害物を避けるときに決定したスライド方向
    private float prevDistToTarget = float.MaxValue; // 前フレームの目標との距離（遠ざかり検知用）
    private float slidingAwayTimer = 0f;             // スライド中に目標から遠ざかり続けた時間

    // コライダーの中心オフセットとサイズ（Rayの起点をコライダー中心に合わせるために使う）
    private Vector2 colliderOffset;
    private float colliderRadius; // CircleCastに使う半径（コライダーの幅の半分）

    // Start はシーン開始時に1回だけ呼ばれる
    private void Start()
    {
        // コライダーの中心・サイズを取得しておく
        // → Rayの開始位置をコライダー中心に合わせることで、端ズレによる誤検知を防ぐ
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            colliderOffset = col.offset;
            // CircleCollider2D なら radius、それ以外は bounds の半径を使う
            if (col is CircleCollider2D circle)
                colliderRadius = circle.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
            else
                colliderRadius = Mathf.Min(col.bounds.extents.x, col.bounds.extents.y);
        }

        // "EnemyWall"タグのオブジェクトの中から、割り当て先を決定する
        GameObject target = FindBestTarget();
        if (target != null)
        {
            targetFloor = target.transform;
            // 割り当て数を+1登録する
            sonsyou s = targetFloor.GetComponent<sonsyou>();
            if (s != null) s.RegisterAssigned(this);

            // 最初の移動方向を目標への方向に設定する
            currentDir = ((Vector2)targetFloor.position - (Vector2)transform.position).normalized;
            // normalizedはピタゴラスの定理を使って目標までの長さを1に統一している
        }
    }

    // 最適な割り当て先（損傷）を選ぶ
    // 優先順位：必要数に達していない損傷の中で、割り当て数が少ない順
    private GameObject FindBestTarget()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("EnemyWall");
        if (walls.Length == 0) return null;

        GameObject best = null;
        int bestAssigned = int.MaxValue;

        foreach (GameObject wall in walls)
        {
            sonsyou s = wall.GetComponent<sonsyou>();
            if (s == null) continue; // sonsyou がないオブジェクトは損傷ではないので無視

            // すでに必要数に達している損傷は除外
            if (s.IsFullyAssigned()) continue;

            int assigned = s.GetAssignedCount();
            if (assigned < bestAssigned)
            {
                bestAssigned = assigned;
                best = wall;
            }
        }

        // 全損傷が満員なら最も近い損傷に向かう（フォールバック）
        // sonsyou コンポーネントがないオブジェクトは対象外にする
        if (best == null)
        {
            float minDist = float.MaxValue;
            foreach (GameObject wall in walls)
            {
                if (wall.GetComponent<sonsyou>() == null) continue; // sonsyouがないEnemyWallは無視
                float dist = Vector2.Distance(transform.position, wall.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    best = wall;
                }
            }
        }

        return best;
    }

    // Update は毎フレーム呼ばれる
    private void Update()
    {
        // 到着済みか目標がなければ何もしない
        if (arrived || targetFloor == null) return;

        Vector2 pos = (Vector2)transform.position;
        // 目標への方向を毎フレーム計算する
        Vector2 toTarget = ((Vector2)targetFloor.position - pos).normalized;

        // 目標方向にRayを飛ばして障害物があるか調べる
        // Physics2D.Raycast(開始位置, 方向, 距離, レイヤー) で障害物を検知する
        RaycastHit2D hit = Physics2D.Raycast(pos, toTarget, rayLength, obstacleLayer);

        Vector2 desiredDir;

        if (hit.collider != null && hit.collider.gameObject != targetFloor.gameObject)
        {
            // 障害物に当たった場合、最初のフレームだけスライド方向を決定する
            // 2回目以降は決定済みの方向を使い続ける（毎フレーム変えると挙動がぶれるため）
            if (committedSlide == Vector2.zero)
            {
                // 障害物の法線（面に対して垂直な方向）から左右2方向のスライド方向を計算する
                Vector2 surfaceA = new Vector2(-hit.normal.y, hit.normal.x);
                Vector2 surfaceB = new Vector2(hit.normal.y, -hit.normal.x);
                // 目標方向により近い方のスライド方向を選ぶ
                // Vector2.Dot(A, B) はAとBの内積（同じ方向ほど大きい値になる）
                committedSlide = Vector2.Dot(toTarget, surfaceA) > 0 ? surfaceA : surfaceB;
                prevDistToTarget = float.MaxValue; // 方向決定時にリセット
                slidingAwayTimer = 0f;
            }

            // 選んだスライド方向も塞がれていたら逆方向に切り替える
            RaycastHit2D slideHit = Physics2D.Raycast(pos, committedSlide, rayLength * 0.5f, obstacleLayer);
            if (slideHit.collider != null && slideHit.collider.gameObject != targetFloor.gameObject)
            {
                committedSlide = -committedSlide;
                slidingAwayTimer = 0f;
            }

            // スライド中に目標から遠ざかり続けていたら逆方向に切り替える
            // 壁の端の法線が誤った方向を返したときの救済処理
            float currentDist = Vector2.Distance(pos, targetFloor.position);
            if (currentDist > prevDistToTarget)
            {
                slidingAwayTimer += Time.deltaTime;
                if (slidingAwayTimer > 0.5f) // 0.5秒以上遠ざかり続けたら逆転
                {
                    committedSlide = -committedSlide;
                    slidingAwayTimer = 0f;
                }
            }
            else
            {
                slidingAwayTimer = 0f;
            }
            prevDistToTarget = currentDist;

            desiredDir = committedSlide;
        }
        else
        {
            // 障害物がなければスライド方向をリセットして目標に向かって直進する
            committedSlide = Vector2.zero;
            slidingAwayTimer = 0f;
            prevDistToTarget = float.MaxValue;
            desiredDir = toTarget;
        }

        // Vector2.Lerp(A, B, t) でAからBに向けてtの割合で補間する
        // 急に方向転換せずなめらかに曲がるようにするための処理
        currentDir = Vector2.Lerp(currentDir, desiredDir, Time.deltaTime * 8f);
        if (currentDir.sqrMagnitude < 0.001f) currentDir = toTarget; // ゼロになったら直進に戻す
        currentDir.Normalize(); // 長さを1に正規化して方向だけ取り出す

        Vector2 move = currentDir * speed * Time.deltaTime;

        // 移動先に障害物があればめり込まないよう移動量を補正する
        RaycastHit2D moveHit = Physics2D.Raycast(pos, move.normalized, move.magnitude + 0.15f, obstacleLayer);
        if (moveHit.collider != null && moveHit.collider.gameObject != targetFloor.gameObject)
        {
            // 障害物にめり込む分の移動量を法線方向に押し戻す
            float overlap = Vector2.Dot(move, -moveHit.normal);
            if (overlap > 0)
                move += moveHit.normal * overlap;
        }

        // 計算した移動量を位置に加算して実際に動かす
        transform.position += (Vector3)move;

        // 目標との距離が1.0未満になったら到着とみなす
        if (Vector2.Distance(transform.position, targetFloor.position) < 1.0f)
        {
            arrived = true;
            // ?.はnullチェック付きのメソッド呼び出し（sonsyouがなければ何もしない）
            // sonsyouに「血小板が到着した」と通知する
            targetFloor.GetComponent<sonsyou>()?.OnPlateletArrived(this);
        }
    }
}