using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS1;
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
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KS1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                ColorBlock cb = KS1.colors;
                cb.normalColor = newColor;
                KS1.colors = cb;
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
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                sonsyou.healTimer = 4.5f;
                KS1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(15);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}