using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilHR3: MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button HR3;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilHR2.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                HR3.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                ColorBlock cb = HR3.colors;
                cb.normalColor = newColor;
                HR3.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilHR2.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 60)
            {
                hakekkyuu.detectionRange = 10f;
                HR3.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(60);
                effectParticle.Play();
            }
        }
    }

}