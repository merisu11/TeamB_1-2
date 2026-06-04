using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS2;
    public SkilPS1 skilPS1;
    public GameObject obj;
    private void Update()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                
                PS2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Player.speed = 9;
            Oxygyn.speed = 14;
            PS2.interactable = false;
            ButtonONOFF = true;
        }
    }

}