using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO5;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor;
    public Color newtextColor;
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
        if (SkilSO4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                SO5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                ColorBlock cb = SO5.colors;
                cb.normalColor = newColor;
                SO5.colors = cb;
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
        if (SkilSO4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                OxygenSpawner.spawnCount = 40;
                SO5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(100);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}