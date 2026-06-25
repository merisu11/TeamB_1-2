using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHT3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HT3;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilHT2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HT3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = HT3.colors;
                cb.normalColor = newColor;
                HT3.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHT2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                hakekkyuu.attachDuration = 4;
                HT3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
            }
            }
    }

}