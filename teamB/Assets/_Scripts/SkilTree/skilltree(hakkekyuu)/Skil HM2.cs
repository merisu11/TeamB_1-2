using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM2;
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
                HM2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                ColorBlock cb = HM2.colors;
                cb.normalColor = newColor;
                HM2.colors = cb;
            }
        }
    }

    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                HM2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(35);
                effectParticle.Play();
                image.fillAmount = 18 / 60f;
                SkilHM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}