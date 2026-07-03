using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueTheGame : MonoBehaviour
{
    //ボタンをクリックした時に呼び出すメソッド
    public void OnClickStart()
    {
        //""の部分は移動したいシーン名
        SceneManager.LoadScene("MainGame");
    }
}
