using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPO1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button PO1;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilKM1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                PO1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                ColorBlock cb = PO1.colors;
                cb.normalColor = newColor;
                PO1.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
            Player.maxOxygen = 2;
            SubPlayer.maxOxygen = 2;
            PO1.interactable = false;
            ButtonONOFF = true;
            GameManager.Instance.RemoveOxygen(10);
            effectParticle.Play();
            }
        }
    }

}