using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM5;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilKM4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            { 
                KM5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                ColorBlock cb = KM5.colors;
                cb.normalColor = newColor;
                KM5.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                KM5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(120);
                effectParticle.Play();
                image.fillAmount = 45 / 45f;
            }
        }
    }

}