using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ktext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenText;
    public static int kessyouban;

    void Update()
    {
        oxygenText.text = kessyouban + "/60";
    }
}