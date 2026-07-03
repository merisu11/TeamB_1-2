using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SkilHM1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HM1;
    public Color newColor;
    [SerializeField] TextMeshProUGUI Text;
    public Color textColor = new Color32(255,0,0,255);
    public Color newtextColor = new Color32(255,255,255,255);
    public Image image;
    public GameObject light;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;
    public static float SavedFillAmount = 0f;
    [SerializeField] private ParticleSystem effectParticle;

    private void Start()
    {
        image.fillAmount = SavedFillAmount;
        light.SetActive(false);
        Text.color = textColor;
    }


    private void Update()
    {
        if (ButtonONOFF)
        {
            HM1.interactable = false; 
        }
        if (GameManager.Instance.TotalOxygen >= 1)
        {
            ColorBlock cb = HM1.colors;
            cb.normalColor = newColor;
            HM1.colors = cb;
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

    public void OnTouched()
    {
        if (GameManager.Instance.TotalOxygen >= 1)
        {
            hakkekkyuuspawn.count = 3;
            HM1.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(1);
            effectParticle.Play();
            image.fillAmount = 3 / 45f;
            SavedFillAmount = image.fillAmount;
            light.SetActive(false);
            audioSource.PlayOneShot(seClip);
            Htext.hakkekkyuu = 3;
        }
    }

}