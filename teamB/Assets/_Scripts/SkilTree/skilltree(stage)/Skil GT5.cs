using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT5;
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
        if (SkilGT4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                GT5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                ColorBlock cb = GT5.colors;
                cb.normalColor = newColor;
                GT5.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                CountdownTimer.startTime = 35f;
                GT5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(100);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}