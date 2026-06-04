using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO4;
    public SkilSO3 skilSO3;
    public GameObject obj;
    private void Update()
    {
        if (SkilSO3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SO4.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO3.ButtonONOFF)
        {
            OxygenSpawner.spawnCount = 75;
            SO4.interactable = false;
            ButtonONOFF = true;
        }
    }

}