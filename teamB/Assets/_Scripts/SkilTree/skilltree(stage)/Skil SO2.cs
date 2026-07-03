using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO2;
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
        if (SkilSO1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                OxygenSpawner.spawnCount = 25;
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