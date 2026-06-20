using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS1;
    public SkilKM1 skilKM1;
    public GameObject obj;

    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PS1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                Player.speed = 6;
                hakekkyuu.moveSpeed = 4;
                Oxygyn.speed = 11;
                PS1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(10);
            }
        }
    }

}