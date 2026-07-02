using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO2;
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
        if (SkilSO1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                SO2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                ColorBlock cb = SO2.colors;
                cb.normalColor = newColor;
                SO2.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                OxygenSpawner.spawnCount = 48;
                SO2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}