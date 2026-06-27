using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS1;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;

    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PS1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                ColorBlock cb = PS1.colors;
                cb.normalColor = newColor;
                PS1.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                Player.speed = 6;
                hakekkyuu.moveSpeed = 4;
                Oxygyn.speed = 11;
                PS1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(10);
                effectParticle.Play();
            }
        }
    }

}