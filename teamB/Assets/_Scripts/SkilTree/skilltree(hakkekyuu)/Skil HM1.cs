using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM1;

    private void Update()
    {
        if (ButtonONOFF)
        {
            HM1.interactable = false;
        }
    }
    public void OnTouched()
    {
        if (GameManager.Instance.TotalOxygen >= 1)
        {
        HM1.interactable = false;
        ButtonONOFF = true;
        GameManager.Instance.RemoveOxygen(1);
        }
    }

}