using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR2: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR2;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilHR1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HR2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                ColorBlock cb = HR2.colors;
                cb.normalColor = newColor;
                HR2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHR1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 25)
            {
                hakekkyuu.detectionRange = 7.5f;
                HR2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(25);
                effectParticle.Play();
            }
        }
    }

}