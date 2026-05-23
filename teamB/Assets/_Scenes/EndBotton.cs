using UnityEngine;

public class EndBotton : MonoBehaviour
{
    public void OnClickQuit()
    {
     #if UNITY_EDITOR
       UnityEditor.EditorApplication.isPlaying = false; //エディタでのみ再生終了
     #else
        Application.Quit(); // ビルドしたアプリを終了
     #endif
    }
}
