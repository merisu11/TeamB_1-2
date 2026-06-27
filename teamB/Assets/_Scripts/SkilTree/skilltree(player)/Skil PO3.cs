using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO3;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilPO2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PO3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                ColorBlock cb = PO3.colors;
                cb.normalColor = newColor;
                PO3.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilPO2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 50)
            {
                Player.maxOxygen = 5;
                SubPlayer.maxOxygen = 5;
                PO3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(50);
                effectParticle.Play();
            }
        }
    }

}