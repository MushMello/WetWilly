using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AS_PlayerHandler : TopDownPlayerHandler
{
    [Header("Asteroid Controls")]
    [SerializeField] private float fireRateSeconds = 1;
    [Header("Game References")]
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private AudioClip damageSound;
    
    private AS_GameHandler gameHandler;
    private float fireTimer = 0f;
    private Coroutine fireTimerRoutine;
    private Vector2 lastDirection;
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        fireTimerRoutine = StartCoroutine(GetTimerRoutine());
        lastDirection = new Vector2(transform.up.x, transform.up.y);
    }

    private IEnumerator GetTimerRoutine()
    {
        while(true)
        {
            float secondsToWait = Mathf.Min(1, fireTimer);
            yield return new WaitForSeconds(secondsToWait);
            fireTimer -= secondsToWait;
        }
    }

    protected override void Update()
    {
        base.Update();

        Vector2 direction = GetInputVector();
        if (direction == Vector2.zero)
        {
            direction = lastDirection;
        }
        else
        {
            lastDirection = direction;
        }

        if (fireAction && !Locked && fireTimer <= 0f && bulletObject && fireAction.action.IsPressed())
        {
            fireTimer = fireRateSeconds;
            GameObject bulletClone = Instantiate(bulletObject, transform.parent);
            bulletClone.transform.localPosition = transform.localPosition;
            AS_WaterHandler waterClone = bulletClone.GetComponent<AS_WaterHandler>();
            if(waterClone)
            {
                waterClone.RegisterPlayerHandler(this);
                waterClone.Send(direction);
            }
        }
    }


    public void RegisterGameHandler(AS_GameHandler gameHandler)
    {
        this.gameHandler = gameHandler;
    }

    public void Hit()
    {
        if(gameHandler)
        {
            gameHandler.RegisterDamage();
        }
        if(audioSource && damageSound && !audioSource.isPlaying)
        {
            SettingsHandler settings = SettingsHandler.GetSettingsHandler();
            if(settings)
            {
                audioSource.volume = settings.MixedEffectVolume;
            }
            audioSource.clip = damageSound;
            audioSource.Play();
        }
    }

    public void Score(int points)
    {
        if(gameHandler)
        {
            gameHandler.Points += points;
        }
    }

    private void OnDestroy()
    {
        if(fireTimerRoutine != null)
        {
            StopCoroutine(fireTimerRoutine);
        }
    }
}
