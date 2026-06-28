using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM2;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;
    
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KM2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                ColorBlock cb = KM2.colors;
                cb.normalColor = newColor;
                KM2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                kessyoubannspawn.count = 18;
                KM2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(35);
                effectParticle.Play();
                image.fillAmount = 18 / 60f;
                SkilKM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}