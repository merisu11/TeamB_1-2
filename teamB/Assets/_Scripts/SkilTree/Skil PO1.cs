using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO1;
    public SkilPS1 skilPS1;
    public GameObject obj;
    private void Update()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                PO1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Oxygyntest.Max_oxygyns = 2;
            PO1.interactable = false;
            ButtonONOFF = true;
        }
    }

}