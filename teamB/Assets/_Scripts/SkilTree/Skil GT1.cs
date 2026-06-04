using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT1;

    private void Update()
    {
        if (ButtonONOFF)
        {
            GT1.interactable = false;
        }
    }
    public void OnTouched()
    {
        CountdownTimer.startTime = 13f;
        GT1.interactable = false;
        ButtonONOFF = true;
    }

}