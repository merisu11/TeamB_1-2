using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO1;
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
                SO1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                ColorBlock cb = SO1.colors;
                cb.normalColor = newColor;
                SO1.colors = cb;
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
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                OxygenSpawner.spawnCount = 35;
                SO1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(15);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}