using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR1: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR1;
    public SkilHT1 skilHT1;
    public GameObject obj;
    private void Update()
    {
        if (SkilHT1.ButtonONOFF)
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
        if (SkilHT1.ButtonONOFF)
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