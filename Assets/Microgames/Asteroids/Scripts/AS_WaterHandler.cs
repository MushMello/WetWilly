using System.Collections;
using UnityEngine;

public class AS_WaterHandler : MonoBehaviour
{
    [Header("Water Controls")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float secondsTilDecay = 5f;

    private AS_PlayerHandler shooterHandler;
    private Rigidbody2D rb;
    private Coroutine decayRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RegisterPlayerHandler(AS_PlayerHandler handler)
    {
        shooterHandler = handler;
    }

    public void Send(Vector2 direction)
    {
        if(rb)
        {
            rb.AddForce(direction * speed);
        }
        decayRoutine = StartCoroutine(GetDecayRoutine());
    }

    private IEnumerator GetDecayRoutine()
    {
        float timer = secondsTilDecay;
        while (timer > 0)
        {
            if(timer >= 1)
            {
                yield return new WaitForSeconds(1);
                timer -= 1;
            }
            else
            {
                yield return new WaitForSeconds(timer);
                timer = 0;
            }
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AS_AsteroidHandler target = collision.gameObject.GetComponent<AS_AsteroidHandler>();
        if (target)
        {
            if(shooterHandler)
            {
                switch (target.AsteroidType)
                {
                    case AsteroidType.Small:
                        shooterHandler.Score(3);
                        break;
                    case AsteroidType.Medium:
                        shooterHandler.Score(2);
                        break;
                    case AsteroidType.Large:
                        shooterHandler.Score(1);
                        break;
                }
            }
            target.Hit();
            Destroy(gameObject);
        }
    }
}
