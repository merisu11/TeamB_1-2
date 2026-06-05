using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR2: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR2;
    public SkilHR1 skilHR1;
    public GameObject obj;
    private void Update()
    {
        if (SkilHR1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                HR2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHR1.ButtonONOFF)
        {
            hakekkyuu.detectionRange = 7.5f;
            HR2.interactable = false;
            ButtonONOFF = true;
        }
    }

}