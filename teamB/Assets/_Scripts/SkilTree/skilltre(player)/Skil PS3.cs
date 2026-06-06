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
            if (GameManager.Instance.TotalOxygen >= 50) { 
            Player.speed = 10;
            hakekkyuu.moveSpeed = 6;
            Oxygyn.speed = 20;
            PS3.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(50);
            }
        }
    }

}