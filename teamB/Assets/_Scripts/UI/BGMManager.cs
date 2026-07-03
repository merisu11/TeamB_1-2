using UnityEngine;

/// <summary>
/// シーンをまたいで再生し続けるBGM管理用のシングルトン。
/// 最初に読み込まれるシーン（タイトル画面など）に1つだけ配置しておけば、
/// 別のシーンに遷移してもこのGameObjectは破棄されず、BGMが途切れず流れ続ける。
///
/// 【使い方】
/// 1. 空のGameObjectを作成し「BGMManager」などの名前を付ける
/// 2. このスクリプトをアタッチする（AudioSourceは自動で追加される）
/// 3. InspectorのBgm Clipに再生したい音楽ファイルをセットする
/// 4. 最初に表示されるシーン（タイトル画面など）にだけ置けばOK。
///    他のシーンには置かなくても、このGameObjectが自動的に引き継がれる
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [Tooltip("最初から再生するBGM")]
    [SerializeField] private AudioClip bgmClip;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        // すでに他のBGMManagerが存在する場合は、自分（新しく生成された方）を破棄して重複を防ぐ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        if (bgmClip != null)
        {
            PlayBGM(bgmClip);
        }
    }

    // 指定したBGMを再生する。すでに同じ曲が再生中なら何もしない（頭出しされて不自然に途切れるのを防ぐ）
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void SetVolume(float value)
    {
        volume = Mathf.Clamp01(value);
        audioSource.volume = volume;
    }
}
