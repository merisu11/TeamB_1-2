using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM1;
    public Color newColor;
    public Image image;
    public static float SavedFillAmount = 0f;
    [SerializeField] private ParticleSystem effectParticle;

    private void Start()
    {
        image.fillAmount = SavedFillAmount;
    }


    private void Update()
    {
        if (ButtonONOFF)
        {
            HM1.interactable = false; 
        }
        if (GameManager.Instance.TotalOxygen >= 1)
        {
            ColorBlock cb = HM1.colors;
            cb.normalColor = newColor;
            HM1.colors = cb;
        }
    }

    public void OnTouched()
    {
        if (GameManager.Instance.TotalOxygen >= 1)
        {
            hakkekkyuuspawn.count = 3;
            HM1.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(1);
            effectParticle.Play();
            image.fillAmount = 3 / 45f;
            SavedFillAmount = image.fillAmount;
        }
    }

}