using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS2;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilPS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PS2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                ColorBlock cb = PS2.colors;
                cb.normalColor = newColor;
                PS2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 25) { 
            Player.speed = 8;
            hakekkyuu.moveSpeed = 5;
            Oxygyn.speed = 9;
            PS2.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(25);
            effectParticle.Play();
            }
        }
    }

}