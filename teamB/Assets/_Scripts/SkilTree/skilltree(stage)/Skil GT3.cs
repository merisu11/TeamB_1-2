using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT3;
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
        if (SkilGT2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {

                GT3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                ColorBlock cb = GT3.colors;
                cb.normalColor = newColor;
                GT3.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                CountdownTimer.startTime = 25f;
                GT3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(45);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}