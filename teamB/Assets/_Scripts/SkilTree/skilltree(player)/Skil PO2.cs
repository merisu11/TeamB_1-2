using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO2;
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
        if (SkilPO1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PO2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 20)
            {
                ColorBlock cb = PO2.colors;
                cb.normalColor = newColor;
                PO2.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPO1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 20)
            {
                Player.maxOxygen = 3;
                SubPlayer.maxOxygen = 3;
                PO2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(20);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}