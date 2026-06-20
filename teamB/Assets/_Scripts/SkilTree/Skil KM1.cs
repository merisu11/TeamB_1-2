using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM1;
    public Color newColor;
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
            if (GameManager.Instance.TotalOxygen >= 1)
            {
                ColorBlock cb = KM1.colors;
                cb.normalColor = newColor;
                KM1.colors = cb;
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