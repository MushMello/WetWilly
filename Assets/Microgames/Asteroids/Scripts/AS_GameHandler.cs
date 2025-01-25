using System.Collections;
using UnityEngine;

public class AS_GameHandler : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float cameraSize = 5f;
    [SerializeField] private Vector2 targetResolution = Vector2.zero;
    [SerializeField] private int health = 3;
    [SerializeField] private int totalRounds = 3;
    [SerializeField] private int initialSpawnCount = 5;
    [SerializeField] private int spawnCountAdditive = 2;
    [Header("References")]
    [SerializeField] private GameObject starField;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AS_PlayerHandler playerHandler;
    [SerializeField] private GameObject[] asteroidPrefabs;

    private Vector2 screenSize = Vector2.zero;
    private bool running = true;
    private bool won = false;
    private int points = 0;
    private int currentRound = 0;
    private ArrayList currentAsteroids = new ArrayList();

    private void Awake()
    {
        if(playerHandler)
        {
            playerHandler.RegisterGameHandler(this);
        }
    }

    private void Start()
    {
        if (targetResolution.x > targetResolution.y)
        {
            screenSize = new Vector2(cameraSize * (targetResolution.x / targetResolution.y), cameraSize);
        }
        else if (targetResolution.y < targetResolution.x)
        {
            screenSize = new Vector2(cameraSize, cameraSize * (targetResolution.y / targetResolution.x));
        }
        else
        {
            screenSize = new Vector2(cameraSize, cameraSize);
        }

        if(gameMusic)
        {
            MusicHandler musicHandler = MusicHandler.GetMusicHandler();
            if(musicHandler)
            {
                musicHandler.MusicClip = gameMusic;
            }
        }
    }

    private void Update()
    {
        if(starField)
        {
            foreach (Transform eachTransform in starField.transform)
            {
                Vector3 pos = eachTransform.localPosition;
                if (pos.x > screenSize.x)
                {
                    pos.x = -1 * screenSize.x;
                }
                if(pos.x < -1 * screenSize.x)
                {
                    pos.x = screenSize.x;
                }
                if(pos.y > screenSize.y)
                {
                    pos.y = -1 * screenSize.y;
                }
                if(pos.y < -1 * screenSize.y)
                {
                    pos.y = screenSize.y;
                }
                eachTransform.localPosition = pos;
            }
        }
        if(currentAsteroids.Count == 0 && running)
        {
            if(currentRound == totalRounds)
            {
                running = false;
                won = true;
            }
            else
            {
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        int spawnCountThisRound = (currentRound++ * spawnCountAdditive) + initialSpawnCount;
        for (int i = 0; i < spawnCountThisRound; i++)
        {
            switch(Random.Range(0,3))
            {
                case 0:
                    SpawnAsteroid(AsteroidType.Small);
                    break;
                case 1:
                    SpawnAsteroid(AsteroidType.Medium);
                    break;
                case 2:
                    SpawnAsteroid(AsteroidType.Large);
                    break;
            }
        }
    }

    private void SpawnAsteroid(AsteroidType asteroidType)
    {
        ArrayList eligibleAsteroids = new ArrayList();
        foreach(GameObject eachObject in asteroidPrefabs)
        {
            AS_AsteroidHandler eachAsteroid = eachObject.GetComponent<AS_AsteroidHandler>();
            if(eachAsteroid && eachAsteroid.AsteroidType == asteroidType)
            {
                eligibleAsteroids.Add(eachObject);
            }
        }
        if(eligibleAsteroids.Count == 0)
        {
            return;
        }

        GameObject asteroidClone = Instantiate((GameObject)eligibleAsteroids[Random.Range(0, eligibleAsteroids.Count)], starField.transform);
        AS_AsteroidHandler newAsteroid = asteroidClone.GetComponent<AS_AsteroidHandler>();
        if(newAsteroid)
        {
            newAsteroid.RegisterGameHandler(this);
            currentAsteroids.Add(newAsteroid);
        }

        asteroidClone.transform.localPosition = new Vector3(Random.Range(screenSize.x * -1, screenSize.x), Random.Range(screenSize.y * -1, screenSize.y), 0);
    }
    
    public void RemoveAsteroid(AS_AsteroidHandler targetAsteroid)
    {
        currentAsteroids.Remove(targetAsteroid);
    }

    public int Health
    {
        get
        {
            return health;
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

    public void RegisterDamage()
    {
        health--;
        if(health <= 0)
        {
            running = false;
            playerHandler.Locked = true;
        }
    }

    public bool Running
    {
        get
        {
            return running;
        }
        set
        {
            running = value;
        }
    }
}
