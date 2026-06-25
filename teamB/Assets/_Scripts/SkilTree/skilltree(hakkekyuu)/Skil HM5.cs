using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM5;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;

    private void Update()
    {
        if (SkilHM4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HM5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                ColorBlock cb = HM5.colors;
                cb.normalColor = newColor;
                HM5.colors = cb;
            }
        }
    }

    public void OnTouched()
    {
        if (SkilHM4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                HM5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(120);
                effectParticle.Play();
                image.fillAmount = 60 / 60f;
            }
        }
    }

}