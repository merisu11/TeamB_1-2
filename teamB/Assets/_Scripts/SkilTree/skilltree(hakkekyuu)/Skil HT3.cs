using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHT3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HT3;
    public SkilHT2 skilHT2;
    public GameObject obj;
    private void Update()
    {
        if (SkilHT2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                HT3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHT2.ButtonONOFF)
        {
            Oxygyntest.Max_oxygyns = 4;
            HT3.interactable = false;
            ButtonONOFF = true;
        }
    }

}