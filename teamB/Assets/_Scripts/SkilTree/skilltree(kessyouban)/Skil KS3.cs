using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class SkilKS3 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button KS3;
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
                KS3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                ColorBlock cb = KS3.colors;
                cb.normalColor = newColor;
                KS3.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKS2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                KS3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(60);
                effectParticle.Play();
            }
        }
    }

}