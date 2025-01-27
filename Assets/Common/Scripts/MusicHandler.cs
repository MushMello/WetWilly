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

    private void Awake()
    {
        if (!musicHandler)
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
        else
        {
            musicHandler.MusicClip = initialMusicClip;
        }
    }


    public AudioClip MusicClip
    {
        set
        {
            if(audioSource)
            {
                musicClip = value;
                audioSource.Stop();
                audioSource.clip = musicClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public float Volume
    {
        get
        {
            if(audioSource)
            {
                return audioSource.volume;
            }
            return 0f;
        }
        set
        {
            if(audioSource)
            {
                audioSource.volume = Mathf.Clamp(value, 0f, 1f);
            }
        }
    }
}
