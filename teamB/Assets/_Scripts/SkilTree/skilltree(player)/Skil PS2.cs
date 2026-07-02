using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPS2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PS2;
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
        if (SkilPS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PS2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                ColorBlock cb = PS2.colors;
                cb.normalColor = newColor;
                PS2.colors = cb;
                if (!ButtonONOFF)
                {
                    light.SetActive(true);
                }
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPS1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 25) { 
                Player.speed = 8;
                hakekkyuu.moveSpeed = 5;
                Oxygyn.speed = 9;
                PS2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(25);
                effectParticle.Play();
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
            }
        }
    }

}