using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KM1;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor;
    public Color newtextColor;
    public GameObject obj;
    public Image image;
    public GameObject light;
    public static float SavedFillAmount = 0f;
    [SerializeField] private ParticleSystem effectParticle;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;

    private void Start()
    {
        image.fillAmount = SavedFillAmount;
        light.SetActive(false);
        Text.color = textColor;
    }
    private void Update()
    {
        if (SkilHM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KM1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 1)
            {
                ColorBlock cb = KM1.colors;
                cb.normalColor = newColor;
                KM1.colors = cb;
                Text.color = newtextColor;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
            else
            {
                light.SetActive(false);
                if (!ButtonONOFF)
                {
                    Text.color = textColor;
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 1)
            {
                kessyoubannspawn.count = 10;
                KM1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(1);
                effectParticle.Play();
                image.fillAmount = 10 / 60f;
                SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
                Ktext.kessyouban = 10;
            }
        }
    }

}