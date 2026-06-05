using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR3: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR3;
    public SkilHR2 skilHR2;
    public GameObject obj;
    private void Update()
    {
        if (SkilHR2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                HR3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHR2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                hakekkyuu.detectionRange = 10f;
            HR3.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(60);
        }
        }
    }

}