using UnityEngine;

public enum AsteroidType
{
    Small,
    Medium,
    Large
}

public class AS_AsteroidHandler : MonoBehaviour
{
    [Header("Asteroid Controls")]
    [SerializeField] private float minStrength = 30f;
    [SerializeField] private float maxStrength = 100f;
    [SerializeField] private float minTorque = 10f;
    [SerializeField] private float maxTorque = 20f;
    [SerializeField] private AsteroidType asteroidType;

    private AS_GameHandler gameHandler;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb)
        {
            float initialStrength = Random.Range(minStrength, maxStrength);
            rb.AddForce(new Vector2((Random.value - .5f) * 2f, (Random.value - .5f) * 2f) * initialStrength);
            rb.AddTorque(Random.Range(minTorque, maxTorque));
        }
    }

    public void RegisterGameHandler(AS_GameHandler gameHandler)
    {
        this.gameHandler = gameHandler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AS_PlayerHandler targetPlayer = collision.gameObject.GetComponent<AS_PlayerHandler>();
        if (targetPlayer)
        {
            targetPlayer.Hit();
            Hit();
        }
    }

    public AsteroidType AsteroidType
    {
        get
        {
            return asteroidType;
        }
    }

    public void Hit()
    {
        if (gameHandler)
        {
            if(asteroidType == AsteroidType.Large)
            {
                gameHandler.SpawnAsteroidAtPosition(AsteroidType.Medium, new Vector2(transform.position.x, transform.position.y));
                gameHandler.SpawnAsteroidAtPosition(AsteroidType.Medium, new Vector2(transform.position.x, transform.position.y));
            }
            else if (asteroidType == AsteroidType.Medium)
            {
                gameHandler.SpawnAsteroidAtPosition(AsteroidType.Small, new Vector2(transform.position.x, transform.position.y));
                gameHandler.SpawnAsteroidAtPosition(AsteroidType.Small, new Vector2(transform.position.x, transform.position.y));
            }
            gameHandler.RemoveAsteroid(this);
        }
        Destroy(gameObject);
    }
}
