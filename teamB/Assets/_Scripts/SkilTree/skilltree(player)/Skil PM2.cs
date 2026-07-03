using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilPM2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PM2;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor = new Color32(255, 0, 0, 255);
    public Color newtextColor = new Color32(255, 255, 255, 255);
    public GameObject obj;
    public Image image;
    public GameObject light;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;
    [SerializeField] private ParticleSystem effectParticle;
    private void Start()
    {
        light.SetActive(false);
        Text.color = textColor;
    }
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
        if (SkilPM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 35)
            {
                SubPlayerSpawner.subPlayerCount = 2;
                PM2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(35);
                effectParticle.Play();
                image.fillAmount = 3 / 10f;
                SkilPM1.SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
                Stext.sekkekkyuu = 3;
            }
        }
    }

}