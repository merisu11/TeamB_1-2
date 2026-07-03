using UnityEngine;

public class skillcomplete : MonoBehaviour
{
    public static bool skillallget = false;

    [Tooltip("「コンプリート」を表示するTextMeshProUGUIなどが付いたGameObject")]
    [SerializeField] private GameObject completeTextObject;

    // このスクリプトが付いたGameObject(=SkillTreeシーンが読み込まれた瞬間)に呼ばれる
    void OnEnable()
    {
        RefreshCompleteDisplay();
    }

    void Update()
    {
        if (SkilGT5.ButtonONOFF)
        {
            if (SkilSO5.ButtonONOFF)
            {
                if (SkilPS3.ButtonONOFF)
                {
                    if (SkilPO3.ButtonONOFF)
                    {
                        if (SkilKS3.ButtonONOFF)
                        {
                            if (SkilKM5.ButtonONOFF)
                            {
                                if (SkilHT3.ButtonONOFF)
                                {
                                    if (SkilHR3.ButtonONOFF)
                                    {
                                        if (SkilHM5.ButtonONOFF)
                                        {
                                            if (SkilPM5.ButtonONOFF)
                                            {
                                                skillallget = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // 全スキル獲得済みなら、まだ非表示なら即座に「コンプリート」を表示する
        // (このUpdateはSkillTreeシーンが開いている間だけ呼ばれるので、
        //  シーンを開いている最中に最後のスキルを取った場合もその場で反映される)
        if (skillallget && completeTextObject != null && !completeTextObject.activeSelf)
        {
            completeTextObject.SetActive(true);
        }
    }

    private void RefreshCompleteDisplay()
    {
        if (completeTextObject == null)
        {
            Debug.LogWarning("skillcomplete: completeTextObjectが設定されていません");
            return;
        }

        completeTextObject.SetActive(skillallget);
    }
}