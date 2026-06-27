using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHT2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HT2;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilHT1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HT2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                ColorBlock cb = HT2.colors;
                cb.normalColor = newColor;
                HT2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHT1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                hakekkyuu.attachDuration = 3;
                HT2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(25);
                effectParticle.Play();
            }
        }
    }

}