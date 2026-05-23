using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilUI1 : MonoBehaviour
{
    static bool ButtonNONFF = false;
    public Button PS1;

    private void Update()
    {
        if (ButtonNONFF)
        {
            PS1.interactable = false;
        }
    }
    public void OnTouched()
    {
        Playertest.speed = 15;
        PS1.interactable = false;
        ButtonNONFF = true;
    }

}