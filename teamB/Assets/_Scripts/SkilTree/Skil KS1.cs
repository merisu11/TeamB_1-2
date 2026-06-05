using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilKS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS1;
    public SkilKM1 skilKM1;
    public GameObject obj;
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                KS1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Oxygyntest.Max_oxygyns = 2;
            KS1.interactable = false;
            ButtonONOFF = true;
        }
    }

}