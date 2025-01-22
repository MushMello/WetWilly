using UnityEngine;

public class BF_MusicHandler : MonoBehaviour
{
    [Header("Music Controls")]
    [SerializeField] private AudioClip musicClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource && musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
