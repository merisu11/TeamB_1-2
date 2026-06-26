using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS2;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilKS1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                KS2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                ColorBlock cb = KS2.colors;
                cb.normalColor = newColor;
                KS2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKS1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                sonsyou.healTimer = 4f;
                KS2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
                effectParticle.Play();
            }
        }
    }

}