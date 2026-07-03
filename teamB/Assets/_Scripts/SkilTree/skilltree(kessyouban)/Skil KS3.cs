using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS3;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor = new Color32(255, 0, 0, 255);
    public Color newtextColor = new Color32(255, 255, 255, 255);
    public GameObject obj;
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
        if (SkilKS2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KS3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                ColorBlock cb = KS3.colors;
                cb.normalColor = newColor;
                KS3.colors = cb;
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
        if (SkilKS2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                sonsyou.healTimer = 2.5f;
                KS3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(60);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}