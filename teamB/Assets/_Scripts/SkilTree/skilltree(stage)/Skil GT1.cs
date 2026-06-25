using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilGT1 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button GT1;
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
                GT1.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                ColorBlock cb = GT1.colors;
                cb.normalColor = newColor;
                GT1.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilKM1.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 10)
            {
                CountdownTimer.startTime = 13f;
                GT1.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(10);
                effectParticle.Play();
            }
        }
    }

}