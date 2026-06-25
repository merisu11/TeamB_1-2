using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO4 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO4;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilSO3.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                SO4.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 70)
            {
                ColorBlock cb = SO4.colors;
                cb.normalColor = newColor;
                SO4.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO3.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 70)
            {
                OxygenSpawner.spawnCount = 75;
                SO4.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(70);
                effectParticle.Play();
            }
        }
    }

}