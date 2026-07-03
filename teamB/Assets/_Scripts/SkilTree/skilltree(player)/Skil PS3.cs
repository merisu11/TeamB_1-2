using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS3;
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
        if (SkilPS2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PS3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = PS3.colors;
                cb.normalColor = newColor;
                PS3.colors = cb;
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
        if (SkilPS2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                Player.speed = 10;
                hakekkyuu.moveSpeed = 6;
                Oxygyn.speed = 11;
                PS3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}