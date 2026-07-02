using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR1: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR1;
    public Color newColor;
    public GameObject obj;
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
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HR1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                ColorBlock cb = HR1.colors;
                cb.normalColor = newColor;
                HR1.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                hakekkyuu.detectionRange = 6f;
                HR1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(10);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}