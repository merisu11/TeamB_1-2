using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT2;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilGT1.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                GT2.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                ColorBlock cb = GT2.colors;
                cb.normalColor = newColor;
                GT2.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilGT1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 30)
            {
                CountdownTimer.startTime = 18f;
                GT2.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(30);
                effectParticle.Play();
            }
        }
    }

}