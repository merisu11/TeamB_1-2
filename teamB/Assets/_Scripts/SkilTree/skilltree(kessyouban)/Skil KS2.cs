using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS2;
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
        if (SkilKS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KS2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                ColorBlock cb = KS2.colors;
                cb.normalColor = newColor;
                KS2.colors = cb;
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
        if (SkilKS1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                sonsyou.healTimer = 4f;
                KS2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}