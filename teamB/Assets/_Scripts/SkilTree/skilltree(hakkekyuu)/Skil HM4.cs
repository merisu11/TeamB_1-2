using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM4;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;

    private void Update()
    {
        if (SkilHM3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HM4.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 80)
            {
                ColorBlock cb = HM4.colors;
                cb.normalColor = newColor;
                HM4.colors = cb;
            }
        }
    }

    public void OnTouched()
    {
        if (SkilHM3.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 80)
            {
                hakkekkyuuspawn.count = 30;
                HM4.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(80);
                effectParticle.Play();
                image.fillAmount = 30 / 45f;
                SkilHM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}