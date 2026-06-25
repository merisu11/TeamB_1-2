using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSO5 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public Button SO5;
    public Color newColor;
    public GameObject obj;
    [SerializeField] private ParticleSystem effectParticle;
    private void Update()
    {
        if (SkilSO4.ButtonONOFF)
        {
            Destroy(obj);
            if (ButtonONOFF)
            {
                SO5.interactable = false;
            }
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                ColorBlock cb = SO5.colors;
                cb.normalColor = newColor;
                SO5.colors = cb;
            }
        }
    }
    public void OnTouched()
    {
        if (SkilSO4.ButtonONOFF)
        {
            if (GameManager.Instance.TotalOxygen >= 100)
            {
                OxygenSpawner.spawnCount = 100;
                SO5.interactable = false;
                ButtonONOFF = true;
                GameManager.Instance.RemoveOxygen(100);
                effectParticle.Play();
            }
        }
    }

}