using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO2;
    public SkilPO1 skilPO1;
    public GameObject obj;
    private void Update()
    {
        if (SkilPO1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                PO2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPO1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 20)
            {
            Player.maxOxygen = 3;
            SubPlayer.maxOxygen = 3;
            PO2.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(20);
            }
        }
    }

}