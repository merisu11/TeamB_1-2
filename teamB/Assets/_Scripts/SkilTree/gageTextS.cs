using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenText;
    public static int sekkekkyuu;

    void Update()
    {
        oxygenText.text = sekkekkyuu + "/10";
    }
}