using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM3;
    public Color newColor;
    public GameObject obj;
    public Image image;
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
        if (SkilHM2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HM3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = HM3.colors;
                cb.normalColor = newColor;
                HM3.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }

    public void OnTouched()
    {
        if (SkilHM2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                hakkekkyuuspawn.count = 15;
                HM3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
                image.fillAmount = 15 / 45f;
                SkilHM1.SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}