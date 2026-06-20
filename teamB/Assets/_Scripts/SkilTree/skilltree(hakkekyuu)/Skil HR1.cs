using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR1: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR1;
    public SkilKM1 skilKM1;
    public GameObject obj;
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                HR1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                hakekkyuu.detectionRange = 6f;
            HR1.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(10);
            }
        }
    }

}