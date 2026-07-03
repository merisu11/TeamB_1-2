using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkilHM5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM5;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor = new Color32(255, 0, 0, 255);
    public Color newtextColor = new Color32(255, 255, 255, 255);
    public GameObject obj;
    public Image image;
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
        if (SkilHM4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HM5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                ColorBlock cb = HM5.colors;
                cb.normalColor = newColor;
                HM5.colors = cb;
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
        if (SkilHM4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 120)
            {
                hakkekkyuuspawn.count = 45;
                HM5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(120);
                effectParticle.Play();
                image.fillAmount = 45 / 45f;
                SkilHM1.SavedFillAmount = image.fillAmount;
                light.SetActive(false);
                audioSource.PlayOneShot(seClip);
                Htext.hakkekkyuu = 45;
            }
        }
    }

}