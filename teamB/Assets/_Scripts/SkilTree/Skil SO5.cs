using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO5;
    public SkilSO4 skilSO4;
    public GameObject obj;
    private void Update()
    {
        if (SkilSO4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                SO5.interactable = false;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO4.ButtonONOFF)
        {
            OxygenSpawner.spawnCount = 100;
            SO5.interactable = false;
            ButtonONOFF = true;
        }
    }

}