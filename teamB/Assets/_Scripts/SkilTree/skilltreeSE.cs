using UnityEngine;

public class skilltreeSE : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip seClip;

    public void PlaySE()
    {
        audioSource.PlayOneShot(seClip);
    }
}