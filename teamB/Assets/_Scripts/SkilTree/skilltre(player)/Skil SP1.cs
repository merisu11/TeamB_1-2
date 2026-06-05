using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSP1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SP1;
    public SkilPS1 skilPS1;
    public GameObject obj;
    private void Update()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SP1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            SubPlayer.blood_on = true;
            SP1.interactable = false;
            ButtonONOFF = true;
        }
    }

}