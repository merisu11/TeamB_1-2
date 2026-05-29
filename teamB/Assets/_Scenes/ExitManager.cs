using UnityEngine;

public class ExitManager : MonoBehaviour 
{
    [SerializeField] private GameObject confirmPanel;

    //おわるボタンを押したときに呼び出す
    public void OpenConfilrmPanel()
    {
        confirmPanel.SetActive(true); //パネルを表示する
    }

    //いいえボタンを押したときに呼び出す
    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false); //パネルを非表示にする
    }

    //はいボタンを押したときに呼び出す
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //エディター再生終了
#else
    Application.Quit(); //ビルドしたゲームの終了
#endif

    }
}
