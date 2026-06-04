using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM1;
    private void Update()
    {
        if (ButtonONOFF)
        {
            KM1.interactable = false;
        }
    }
    public void OnTouched()
    {
       
        KM1.interactable = false;
        ButtonONOFF = true;
    }

}