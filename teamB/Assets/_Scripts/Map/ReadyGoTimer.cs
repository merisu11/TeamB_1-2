using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyGoTimer : MonoBehaviour
{
    public Text uiText; //画像に表示するTextコンポーネント
    public GameObject startPanel; //一緒に消したいPanel

    void Start()
    {
        //演出のコールチンを開始
        StartCoroutine(ReadyGoRoutine());
    }

    IEnumerator ReadyGoRoutine()
    {
        //1.　最初に　Ready?　を表示
        uiText.text = "Ready?";

        //2.　2秒待つ
        yield return new WaitForSeconds(2.0f);

        //3.　「Ready?」を残さず、「GO!」を追加
        uiText.text = "GO!";

        //4. 2秒後に　GO!　が出てから、さらに1秒待つ
        yield return new WaitForSeconds(1.0f);

        //テキストを空にする
        uiText.text = "";

        //一緒にPanelを非表示（アクティブをオフ）にする
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }
}
