using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHT1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HT1;

    private void Update()
    {
        if (ButtonONOFF)
        {
            HT1.interactable = false;
        }
    }
    public void OnTouched()
    {
        if (GameManager.Instance.TotalOxygen >= 10)
        {
        hakekkyuu.attachDuration = 2.4f;
        HT1.interactable = false;
        ButtonONOFF = true;
        GameManager.Instance.RemoveOxygen(10);
        }
    }

}