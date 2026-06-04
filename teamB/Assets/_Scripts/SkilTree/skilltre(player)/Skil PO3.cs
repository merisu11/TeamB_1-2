using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO3;
    public SkilPO2 skilPO2;
    public GameObject obj;
    private void Update()
    {
        if (SkilPO2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                PO3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPO2.ButtonONOFF)
        {
            Oxygyn.Max_oxygyns = 5;
            PO3.interactable = false;
            ButtonONOFF = true;
        }
    }

}