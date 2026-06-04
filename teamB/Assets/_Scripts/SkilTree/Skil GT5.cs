using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT5;
    public SkilGT4 skilGT4;
    public GameObject obj;
    private void Update()
    {
        if (SkilGT4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                GT5.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT4.ButtonONOFF)
        {
            CountdownTimer.startTime = 35f;
            GT5.interactable = false;
            ButtonONOFF = true;
        }
    }

}