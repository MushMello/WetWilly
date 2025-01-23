using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    private static SettingsHandler settingsHandler;

    [Header("Default Settings")]
    [SerializeField][Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float effectVolume = .5f;
    [SerializeField][Range(0f, 1f)] private float masterVolume = .75f;
    [Header("References")]
    [SerializeField] private MusicHandler musicHandler;

    public static SettingsHandler GetSettingsHandler()
    {
        return settingsHandler;
    }

    private void Start()
    {
        settingsHandler = this;
        UpdateMusicVolume();
        DontDestroyOnLoad(gameObject);
    }

    private void UpdateMusicVolume()
    {
        if(musicHandler)
        {
            musicHandler.Volume = MixedMusicVolume;
        }
    }

    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = Mathf.Clamp(value, 0f, 1f);
            UpdateMusicVolume();
        }
    }

    public float EffectVolume
    {
        get
        {
            return effectVolume;
        }
        set
        {
            effectVolume = Mathf.Clamp(value, 0f, 1f);
        }
    }

    public float MasterVolume
    {
        get
        {
            return masterVolume;
        }
        set
        {
            masterVolume = Mathf.Clamp(value, 0f, 1f);
        }
    }

    public float MixedMusicVolume
    {
        get
        {
            return musicVolume * masterVolume;
        }
    }

    public float MixedEffectVolume
    {
        get
        {
            return effectVolume * masterVolume;
        }
    }
}
