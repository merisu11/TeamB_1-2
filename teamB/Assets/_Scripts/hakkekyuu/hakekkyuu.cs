using UnityEngine;

/// <summary>
/// 白血球クラス
/// 仕様：病原菌を検知したら近づいて張り付き行動を阻害する
/// </summary>
public class hakekkyuu : MonoBehaviour
{
    
    
 
    public enum State
    {
        Follow, // 赤血球（プレイヤー）の後ろをついていく状態
        Chase,  // 病原菌を発見して追いかけている状態
        Attach, // 病原菌に張り付いて阻害している状態
    }

    
    
    
    [Header("検知・妨害")]

    public static float detectionRange = 5f;

    [Tooltip("基本の妨害継続時間（秒）/ スキル未取得: 2秒")]
    public static float attachDuration = 2f;

    [Header("移動")]
    public static float moveSpeed = 5f;

    [Tooltip("追従時、赤血球との目標距離")]
    public float followDistance = 1.2f;

    [Header("集中阻止")]
    [Tooltip("1体の病原菌に同時に張り付ける白血球の最大数")]
    public int maxPerPathogen = 1;

    
    // スキルツリー用のpublicメソッド
    // publicにすることで他のスクリプトから呼び出せる
    
    /// 【スキル①】妨害継続時間を変更する
   
   
    public void SetAttachDuration(float duration)
    {
        // 引数で受け取った値をそのままattachDurationに代入する
        attachDuration = duration;
    }

   
    /// 【スキル②】検知範囲を倍率で変更する
   
    
    public void SetDetectionRangeMultiplier(float multiplier)
    {
        // 初期値（baseDetectionRange）に倍率をかけて現在の検知範囲を更新する
        // 直接detectionRangeに掛け算すると何度も呼ばれたとき値がどんどん大きくなるため
        // 必ず基準値（baseDetectionRange）から計算する
        detectionRange = baseDetectionRange * multiplier;
    }

    private State currentState = State.Follow; // 現在の状態（最初はFollow）
    private Transform playerTransform;         // 追従対象（赤血球）のTransform
    private Transform chaseTarget;             // 追いかけている病原菌のTransform
    private GameObject attachedPathogen;       // 現在張り付いている病原菌のGameObject
    private float attachTimer;                 // 張り付き残り時間のカウントダウン用
    private Rigidbody2D rb;                    // 自分のRigidbody2D（物理・移動に使う）
    private Collider2D col;                    // 自分のCollider2D（当たり判定に使う）
    private float baseDetectionRange;          // 検知範囲の初期値（スキル倍率の基準）
    private FixedJoint2D joint;                // 張り付き用のJoint（接触時に動的に追加する）

   void Start()
    {
        // GetComponent<T>() は自分のGameObjectについているTというコンポーネントを取得する
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // 初期の検知範囲を保存しておく（スキルで倍率をかけるときの基準値として使う）
        baseDetectionRange = detectionRange;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        if (col != null)
        {
            col.isTrigger = false;
        }
        // FindGameObjectWithTag("タグ名") はシーン内からそのタグを持つGameObjectを1つ探す
        // 赤血球にタグ"Player"を設定しておく必要がある
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            // Transform はそのオブジェクトの位置・回転・スケールを持つコンポーネント
            playerTransform = playerObj.transform;
        else
            Debug.LogWarning("[白血球] タグ'Player'のオブジェクトが見つかりません");

        currentState = State.Follow;
    }

      void Update()
    {
        // switch文でcurrentStateの値によって実行する処理を切り替える
        // if-elseでも書けるが状態が多いときはswitchのほうが読みやすい
        switch (currentState)
        {
            case State.Follow:
                UpdateFollow();    // 赤血球に追従する処理
                SearchPathogen();  // 病原菌を探す処理（Follow中だけ行う）
                break;

            case State.Chase:
                UpdateChase();     // 病原菌を追いかける処理
                break;

            case State.Attach:
                UpdateAttach();    // 張り付き中の処理
                break;
        }
    }

  
    // 赤血球（プレイヤー）に追従する処理
  
    void UpdateFollow()
    {
        // nullチェック：playerTransformが取得できていなければ何もしない
        if (playerTransform == null) return;

        // Vector2.Distance(A, B) はAとBの距離を返す
        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (dist > followDistance)
        {
            // 赤血球との距離がfollowDistanceより大きければ近づく
            // normalized は方向ベクトルを長さ1に正規化する（方向だけ取り出す）
            Vector2 dir = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            // linearVelocity に値をセットすることでRigidbody2Dが物理的に移動する
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            // 十分近ければ止まる
            rb.linearVelocity = Vector2.zero;
        }
    }

    
    // 周囲の病原菌を探す処理（Follow状態のときだけ毎フレーム呼ばれる）
    
    void SearchPathogen()
    {
        // Physics2D.OverlapCircleAll(中心, 半径) は
        // 指定した円の範囲内にあるCollider2Dを全て配列で返す
        Collider2D[] allHits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        // foreachで配列の要素を1つずつ取り出して処理する
        foreach (Collider2D hit in allHits)
        {
            // 自分自身のColliderはスキップ（自分を追いかけないようにする）
            if (hit.gameObject == gameObject) continue;

            // LayerMask.LayerToName(レイヤー番号) はレイヤー番号を名前の文字列に変換する
            // "Pathogen"レイヤー以外はスキップ
            if (LayerMask.LayerToName(hit.gameObject.layer) != "Pathogen") continue;

            // 既にこの病原菌に何体張り付いているか数える
            int attached = CountAttachedTo(hit.gameObject);

            // 最大数に達していなければこの病原菌を追いかける
            if (attached < maxPerPathogen)
            {
                chaseTarget = hit.transform;
                currentState = State.Chase; // 状態をChaseに切り替え
                return; // 1体見つかればOKなのでループを抜ける
            }
        }
    }

    
    // 病原菌を追いかける処理（Chase状態のときに毎フレーム呼ばれる）
    
    void UpdateChase()
    {
        // 追いかけている病原菌が消えていたら（倒されたなど）追従に戻る
        if (chaseTarget == null)
        {
            currentState = State.Follow;
            return;
        }

        // 病原菌の方向に向かって移動する
        Vector2 dir = ((Vector2)chaseTarget.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
        // 病原菌との接触検知はOnCollisionEnter2Dに任せる
    }

    
    // OnCollisionEnter2D はCollider同士が衝突した瞬間にUnityが自動で呼ぶ関数
    // 引数のCollision2Dには衝突相手の情報が入っている
    // （IsTriggerがオフのCollider同士が衝突したときに呼ばれる）
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Chase状態のときだけ処理する
        if (currentState != State.Chase) return;

        // chaseTargetがnullなら処理しない
        if (chaseTarget == null) return;

        // 衝突したのが追いかけていた病原菌でなければ処理しない
        // （壁や他のオブジェクトとの衝突は無視する）
        if (collision.gameObject != chaseTarget.gameObject) return;

        // 病原菌に触れた → 張り付き開始
        BeginAttach(collision.gameObject);
    }

    
    // 張り付き開始の処理
    // BeginAttachはOnCollisionEnter2Dから呼ばれる
    
    void BeginAttach(GameObject pathogen)
    {
        attachedPathogen = pathogen;       // 張り付いた病原菌を記録
        attachTimer = attachDuration; // タイマーをセット
        currentState = State.Attach;  // 状態をAttachに切り替え

        // 自分の物理移動を止める
        rb.linearVelocity = Vector2.zero;

        // Kinematicにすることで物理演算（重力・衝突の力）の影響を受けなくなる
        // これにより病原菌をJointで繋いでも押し合いが起きなくなる
        rb.bodyType = RigidbodyType2D.Kinematic;

        // FixedJoint2D をこのGameObjectに動的に追加する
        // AddComponent<T>() はスクリプトからコンポーネントを追加する関数
        joint = gameObject.AddComponent<FixedJoint2D>();

        // connectedBody に病原菌のRigidbody2Dを設定することで
        // 白血球と病原菌を物理的に繋ぐ（白血球が病原菌の表面に固定される）
        joint.connectedBody = pathogen.GetComponent<Rigidbody2D>();

        // autoConfigureConnectedAnchor = true にすると
        // 接触した瞬間の位置関係を自動でアンカー（固定点）として記録してくれる
        // これにより接触した位置にぴったり固定される
        joint.autoConfigureConnectedAnchor = true;

        // IsTriggerをオンにすることで以降は物理衝突を起こさなくなる
        // Jointで繋いだあとは衝突判定が不要なのでオフにしてめり込みを防ぐ
        if (col != null) col.isTrigger = true;

        // GetComponent<IPathogen>() でIPathogenインターフェースを実装しているか確認し
        // SetImpeded(true) を呼んで病原菌の行動を阻害する
        IPathogen script = pathogen.GetComponent<IPathogen>();
        if (script != null)
            script.SetImpeded(true);
        else
            Debug.LogError($"[白血球] IPathogen が見つかりません: {pathogen.name}");
    }

        // 張り付き中の処理（Attach状態のときに毎フレーム呼ばれる）
    
    void UpdateAttach()
    {
        // 病原菌が消滅していたらJointも無効になるので自分も消す
        if (attachedPathogen == null)
        {
            Destroy(gameObject); // Destroy(gameObject) は自分自身を消す
            return;
        }

        // attachDurationの時間が経過したら張り付き終了
        attachTimer -= Time.deltaTime; // Time.deltaTime は前フレームからの経過時間（秒）

        if (attachTimer <= 0f)
        {
            // Jointを削除して物理的な拘束を解除する
            // Destroy(コンポーネント) でそのコンポーネントだけを削除できる
            if (joint != null) Destroy(joint);

            // 病原菌の行動阻害を解除する
            IPathogen script = attachedPathogen.GetComponent<IPathogen>();
            if (script != null) script.SetImpeded(false);

            // 白血球自身を消滅させる
            Destroy(gameObject);
        }
    }

    
    // 指定した病原菌に現在何体の白血球が張り付いているか数えるユーティリティ関数
    // CountAttachedToはSearchPathogenから呼ばれる
        int CountAttachedTo(GameObject pathogen)
    {
        int count = 0;

        // FindObjectsByType<T>() はシーン内の全てのTを配列で返す
        // FindObjectsSortMode.None はソートしない（パフォーマンスが良い）
        foreach (hakekkyuu wbc in FindObjectsByType<hakekkyuu>(FindObjectsSortMode.None))
        {
            // attachedPathogenが同じ病原菌を指しているか確認
            if (wbc.attachedPathogen == pathogen)
                count++;
        }
        return count;
    }

  
    // OnDrawGizmosSelected はUnityエディターでこのオブジェクトを選択したとき
    // Sceneビューに補助図形を描画する関数（ゲーム中には表示されない）
    
    void OnDrawGizmosSelected()
    {
        // 検知範囲を水色のワイヤーフレーム円で表示する
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}