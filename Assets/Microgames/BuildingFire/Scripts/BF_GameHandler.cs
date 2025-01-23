using UnityEngine;

// BF_GameHandler
// handles the overarching game logic in Building Fire

public class BF_GameHandler : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private AudioClip[] damageSounds;

    private bool isRunning = true;
    private int points = 0;
    private int health;
    private AudioSource audioSource;

    private void Start()
    {
        health = startingHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayDamageSound()
    {
        if(audioSource)
        {
            SettingsHandler settings = SettingsHandler.GetSettingsHandler();
            if (settings)
            {
                audioSource.volume = settings.MixedEffectVolume;
            }
            audioSource.clip = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.Play();
        }
    }

    public bool Running
    {
        get
        {
            return isRunning;
        }
        set
        {
            isRunning = value;
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if(value < health)
            {
                PlayDamageSound();
            }
            health = value;
            if(health <= 0)
            {
                Running = false;
            }
        }
    }
}
