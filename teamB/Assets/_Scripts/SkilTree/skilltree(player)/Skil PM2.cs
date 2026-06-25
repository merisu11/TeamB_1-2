using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPM2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PM2;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilPM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PM2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                ColorBlock cb = PM2.colors;
                cb.normalColor = newColor;
                PM2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                SubPlayer.blood_on = true;
                PM2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(35);
                effectParticle.Play();
                image.fillAmount = 3 / 10f;
            }
        }
    }

}