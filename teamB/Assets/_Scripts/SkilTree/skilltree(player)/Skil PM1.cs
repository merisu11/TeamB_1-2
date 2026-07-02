using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkilPM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PM1;
    public Color newColor;
    public GameObject obj;
    public Image image;
    public GameObject prefab;
    public GameObject light;
    public static float SavedFillAmount = 0.1f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;
    [SerializeField] private ParticleSystem effectParticle;

    private void Start()
    {
        image.fillAmount = SavedFillAmount;
        light.SetActive(false);
    }
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PM1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 20)
            {
                ColorBlock cb = PM1.colors;
                cb.normalColor = newColor;
                PM1.colors = cb;
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
            if (GameManager.Instance.TotalOxygen >= 20)
            {
                SubPlayerSpawner.subPlayerCount = 1;
                PM1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(20);
                effectParticle.Play();
                image.fillAmount = 2 / 10f;
                SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }
    

}