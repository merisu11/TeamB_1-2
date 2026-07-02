using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO3;
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
        if (SkilSO2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                SO3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                ColorBlock cb = SO3.colors;
                cb.normalColor = newColor;
                SO3.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 45)
            {
                OxygenSpawner.spawnCount = 60;
                SO3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(45);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}