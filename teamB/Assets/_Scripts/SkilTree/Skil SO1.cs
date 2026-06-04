using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO1;
    public SkilGT1 skilGT1;
    public GameObject obj;
    private void Update()
    {
        if (SkilGT1.ButtonONOFF)
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
        if (SkilGT1.ButtonONOFF)
        {
            OxygenSpawner.spawnCount = 35;
            SO1.interactable = false;
            ButtonONOFF = true;
        }
    }

}