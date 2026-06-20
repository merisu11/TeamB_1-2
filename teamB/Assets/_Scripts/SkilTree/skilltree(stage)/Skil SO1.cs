using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO1;
    public SkilKM1 skilKM1;
    public GameObject obj;
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SO1.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                OxygenSpawner.spawnCount = 35;
            SO1.interactable = false;
            ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(15);
            }
        }
    }

}