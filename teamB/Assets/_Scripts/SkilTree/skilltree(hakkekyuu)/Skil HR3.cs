using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR3: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR3;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor = new Color32(255, 0, 0, 255);
    public Color newtextColor = new Color32(255, 255, 255, 255);
    public GameObject obj;
    public GameObject light;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;
    [SerializeField] private ParticleSystem effectParticle;
    private void Start()
    {
        light.SetActive(false);
        Text.color = textColor;
    }
    private void Update()
    {
        if (SkilHR2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HR3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                ColorBlock cb = HR3.colors;
                cb.normalColor = newColor;
                HR3.colors = cb;
                Text.color = newtextColor;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
            else
            {
                light.SetActive(false);
                if (!ButtonONOFF)
                {
                    Text.color = textColor;
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHR2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                hakekkyuu.detectionRange = 10f;
                HR3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(60);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}