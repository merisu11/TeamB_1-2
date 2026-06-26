using UnityEngine;
using System.Collections;

public class TimePanelHider : MonoBehaviour
{
    //チュートリアルImage
    public Gameobject チュートリアルImage;

    //表示させておく時間
    public float displayTime = 4.0f;

    void Start()
    {
        // ゲームが始まったら、カウントダウンの処理（コルーチン）を開始する
        StartCoroutine(HidePanelAfterDelay());
    }

    IEnumerator HidePanelAfterDelay()
    {
        // 設定した秒数だけ待つ
        yield return new WaitForSeconds(displayTime);

        // 説明画像を非表示にする
        チュートリアルImage.SetActive(false);

    }
}
