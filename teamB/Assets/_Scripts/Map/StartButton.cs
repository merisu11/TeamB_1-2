using UnityEngine;
using UnityEngine.SceneManagement;//シーン切替に必要

public class StartButton : MonoBehaviour
{
    //ボタンをクリックした時に呼び出すメソッド
public void OnClickStart()
    {
        //""の部分は移動したいシーン名
        SceneManager.LoadScene("MainGame");
    }
}
