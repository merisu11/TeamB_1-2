using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilUI3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS3;
    public SkilPS2 skilPS2;
    public GameObject obj;
    private void Update()
    {
        if (SkilPS2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                
                PS3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS2.ButtonONOFF)
        {
            Playertest.speed = 15;
            Oxygyntest.speed = 20;
            PS3.interactable = false;
            ButtonONOFF = true;
        }
    }

}