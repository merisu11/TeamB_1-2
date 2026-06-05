using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT3;
    public SkilGT2 skilGT2;
    public GameObject obj;
    private void Update()
    {
        if (SkilGT2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                GT3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                CountdownTimer.startTime = 25f;
            GT3.interactable = false;
            ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(45);
            }
        }
    }

}