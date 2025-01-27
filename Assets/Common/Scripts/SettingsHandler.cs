using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsHandler : MonoBehaviour
{
    private static SettingsHandler settingsHandler;

    [Header("Default Settings")]
    [SerializeField][Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float effectVolume = .5f;
    [SerializeField][Range(0f, 1f)] private float masterVolume = .75f;
    [Header("References")]
    [SerializeField] private MusicHandler musicHandler;
    [SerializeField] private InputActionReference pauseAction;

    private bool canPause = false;
    private bool isPaused = false;
    private bool hasUnpressedPause = true;
    private GameObject pauseCanvas;

    public static SettingsHandler GetSettingsHandler()
    {
        return settingsHandler;
    }

    private void Start()
    {
        settingsHandler = this;
        UpdateMusicVolume();
        pauseCanvas = transform.Find("PauseCanvas").gameObject;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(hasUnpressedPause && pauseAction && pauseCanvas && pauseAction.action.WasPressedThisFrame() && canPause)
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            hasUnpressedPause = false;
            pauseCanvas.SetActive(isPaused);
        }
        if(pauseAction && pauseAction.action.WasReleasedThisFrame())
        {
            hasUnpressedPause = true;
        }
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

    public bool CanPause
    {
        set
        {
            canPause = value;
        }
    }
}
