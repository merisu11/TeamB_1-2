using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT4;
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