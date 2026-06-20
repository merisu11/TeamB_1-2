using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM1;
    public SkilHM1 skilHM1;
    public GameObject obj;
    private void Update()
    {
        if (SkilHM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KM1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 1)
            {
                KM1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(1);
            }
        }
    }

}