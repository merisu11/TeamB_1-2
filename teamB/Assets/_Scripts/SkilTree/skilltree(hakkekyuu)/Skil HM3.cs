using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM3;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;

    private void Update()
    {
        if (SkilHM2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HM3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = HM3.colors;
                cb.normalColor = newColor;
                HM3.colors = cb;
            }
        }
    }

    public void OnTouched()
    {
        if (SkilHM2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                HM3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
                image.fillAmount = 25 / 60f;
                SkilHM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}