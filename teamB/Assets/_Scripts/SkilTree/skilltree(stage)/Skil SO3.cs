using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO3;
    public SkilSO2 skilSO2 ;
    public GameObject obj;
    private void Update()
    {
        if (SkilSO2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SO3.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                OxygenSpawner.spawnCount = 60;
            SO3.interactable = false;
            ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(45);
            }
        }
    }

}