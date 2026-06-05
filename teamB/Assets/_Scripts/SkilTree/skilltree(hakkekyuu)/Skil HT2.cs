using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHT2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HT2;
    public SkilHT1 skilHT1;
    public GameObject obj;
    private void Update()
    {
        if (SkilHT1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                HT2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHT1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                hakekkyuu.attachDuration = 3;
             HT2.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(25);
            }
        }
    }

}