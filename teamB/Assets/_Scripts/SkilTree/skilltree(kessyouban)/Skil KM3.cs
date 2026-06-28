using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM3;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;
   
    private void Update()
    {
        if (SkilKM2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KM3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = KM3.colors;
                cb.normalColor = newColor;
                KM3.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                kessyoubannspawn.count = 25;
                KM3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
                image.fillAmount = 25 / 60f;
                SkilKM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}