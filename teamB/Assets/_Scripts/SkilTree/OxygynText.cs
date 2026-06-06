using UnityEngine;
using TMPro;

public class OxygenUI : MonoBehaviour
{
    public TMP_Text oxygenText;

    void Update()
    {
        oxygenText.text = GameManager.Instance.TotalOxygen.ToString() ;
    }
}