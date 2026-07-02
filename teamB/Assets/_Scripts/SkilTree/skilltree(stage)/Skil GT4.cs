using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT4;
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
        if (SkilGT3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                GT4.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 70)
            {
                ColorBlock cb = GT4.colors;
                cb.normalColor = newColor;
                GT4.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT3.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 70)
            {
                CountdownTimer.startTime = 30f;
                GT4.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(70);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}