using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS1;
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
                KS1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                ColorBlock cb = KS1.colors;
                cb.normalColor = newColor;
                KS1.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 15)
            {
                KS1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(15);
                effectParticle.Play();
            }
        }
    }

}