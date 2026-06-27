using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPM4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PM4;
    public Color newColor;
    public GameObject obj;
    public Image image;
    [SerializeField] private ParticleSystem effectParticle;
  
    private void Update()
    {
        if (SkilPM3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PM4.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 85)
            {
                ColorBlock cb = PM4.colors;
                cb.normalColor = newColor;
                PM4.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPM3.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 85)
            {
                SubPlayer.blood_on = true;
                PM4.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(85);
                effectParticle.Play();
                image.fillAmount = 7 / 10f;
                SkilPM1.SavedFillAmount = image.fillAmount;
            }
        }
    }

}