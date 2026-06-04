using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS1;

    private void Update()
    {
        if (ButtonONOFF)
        {
            PS1.interactable = false;
        }
    }
    public void OnTouched()
    {
        Player.speed = 6;
        Oxygyn.speed = 11;
        PS1.interactable = false;
        ButtonONOFF = true;
    }

}