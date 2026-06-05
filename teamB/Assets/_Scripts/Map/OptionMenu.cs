using UnityEngine;
using UnityEngine.UI;
using TMPro; //InputField(TMP)を使うために必要

public class OptionMenu : MonoBehaviour
{
    [System.Serializable]
    public class VolumeSet
    {
        public string name; //項目名
        public Slider slider;
        public TMP_InputField inputField;
    }

    [Header("画面の切替--")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    [Header("--音量設定(4項目)--")]
    [SerializeField] private VolumeSet masterVolume;
    [SerializeField] private VolumeSet bgmVolume;
    [SerializeField] private VolumeSet seVolume;
    [SerializeField] private VolumeSet voiceVolume;

    void Start()
    {
        //4つの音量設定を初期化・イベント登録
        SetupVolumeEvents(masterVolume, 80f); //初期値 80
        SetupVolumeEvents(bgmVolume, 70f); //初期値 70
        SetupVolumeEvents(seVolume, 80f); //初期値 80
        SetupVolumeEvents(voiceVolume, 90f); //初期値 90

    }

    //スライダーと入力欄の連動設定を行う関数
    private void SetupVolumeEvents(VolumeSet volume, float defaultValue)
    {
        //初期値を適用
        volume.slider.value = defaultValue;
        volume.inputField.text = defaultValue.ToString();

        //バー(Slider)を動かした時→入力欄の数値を置き換える
        volume.slider.onValueChanged.AddListener((float val) =>
        {
            volume.inputField.text = val.ToString();
            OnVolumeChanged(volume.name, val);
        });

        //入力欄(InputField)を打ち終えた時→バーの位置を書き換える
        volume.inputField.onEndEdit.AddListener((string text) =>
        {
            if (float.TryParse(text, out float result))
            {
                //0～100の範囲に制限する
                result = Mathf.Clamp(result, 0f, 100f);
                volume.slider.value = result;
                volume.inputField.text = result.ToString();
                OnVolumeChanged(volume.name, result);
            }
            else
            {
                //数字以外が打たれたら現在バー値に戻す
                volume.inputField.text = volume.slider.value.ToString();
            }
        });
    }

    //実際に音量が変わったときに実行される処理
    private void OnVolumeChanged(string name, float value)
    {
        Debug.Log($"{name}の音量が{value}に変更されました");

        //ここにAudioMixerやSoundManagerへの音量反映処理を書く
        switch (name)
        {
            case "Master": break;
            case "BGM": break;
            case "SE": break;
            case "Voice": break;
        }
    }
}
