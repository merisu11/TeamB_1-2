using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSP1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SP1;
    public SkilKM1 skilKM1;
    public GameObject obj;
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
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
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 20)
            {
            SubPlayer.blood_on = true;
            SP1.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(20);
            }
        }
    }

}