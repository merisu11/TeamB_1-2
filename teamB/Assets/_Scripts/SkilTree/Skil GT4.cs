using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT4;
    public SkilGT3 skilGT3;
    public GameObject obj;
    private void Update()
    {
        if (SkilGT3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                GT4.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT3.ButtonONOFF)
        {
            CountdownTimer.startTime = 30f;
            GT4.interactable = false;
            ButtonONOFF = true;
        }
    }

}