using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [Header("Music Controls")]
    [SerializeField] private AudioClip initialMusicClip;

    private static MusicHandler musicHandler;

    public static MusicHandler GetMusicHandler()
    {
        return musicHandler;
    }    

    private AudioClip musicClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource)
        {
            musicClip = initialMusicClip;
            if(musicClip)
            {
                audioSource.clip = musicClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        musicHandler = this;
        DontDestroyOnLoad(gameObject);
    }


    public AudioClip MusicClip
    {
        set
        {
            musicClip = value;
            audioSource.Stop();
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
