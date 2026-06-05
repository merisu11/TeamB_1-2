using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT2;
    public SkilGT1 skilGT1;
    public GameObject obj;
    private void Update()
    {
        if (SkilGT1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                GT2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                CountdownTimer.startTime = 18f;
            GT2.interactable = false;
            ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
            }
        }
    }

}