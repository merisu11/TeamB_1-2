using UnityEngine;

public class TitleMenuManager : MonoBehaviour 
{
    //インスペクターから設定パネルを紐づける
    [SerializeField] private GameObject settingPanel;

    //設定画面を開く処理
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    //設定画面を閉じる処理
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

}
