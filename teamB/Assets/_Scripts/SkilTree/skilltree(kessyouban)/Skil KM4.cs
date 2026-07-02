using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM4;
    public Color newColor;
    public GameObject obj;
    public Image image;
    public GameObject light;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;
    [SerializeField] private ParticleSystem effectParticle;
    private void Start()
    {
        light.SetActive(false);
    }
    private void Update()
    {
        if (SkilKM3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KM4.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 80)
            {
                ColorBlock cb = KM4.colors;
                cb.normalColor = newColor;
                KM4.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM3.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 80)
            {
                kessyoubannspawn.count = 40;
                KM4.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(80);
                effectParticle.Play();
                image.fillAmount = 40 / 60f;
                SkilKM1.SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}