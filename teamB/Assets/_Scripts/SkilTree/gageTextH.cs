using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Htext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenText;
    public static int hakkekkyuu;

    void Update()
    {
        oxygenText.text = hakkekkyuu + "/45";
    }
}