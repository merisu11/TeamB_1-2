using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO2;
    public SkilSO1 skilSO1;
    public GameObject obj;
    private void Update()
    {
        if (SkilSO1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SO2.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                OxygenSpawner.spawnCount = 48;
            SO2.interactable = false;
            ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
            }
        }
    }

}