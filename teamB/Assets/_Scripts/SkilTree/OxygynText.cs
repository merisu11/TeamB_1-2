using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenUI : MonoBehaviour
{
    [SerializeField] private Text oxygenText;


    void Update()
    {
        oxygenText.text = GameManager.Instance.TotalOxygen.ToString() ;
    }
}