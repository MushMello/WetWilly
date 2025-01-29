using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 start = new Vector2(-7, 2.75f);
    public Vector2 end = new Vector2(7, 2.75f);

    public GameObject prefab;

    public float enemySpeed = 5f;

    Vector2 currentCoordinates;
    bool isMovingRight;

    void Start()
    {
        currentCoordinates = start;
        
    }

    public void playExplosion()
    {
        ParticleSystem parts = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(parts, totalDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMovingRight)
        {
            currentCoordinates = Vector2.MoveTowards(currentCoordinates, end, Time.deltaTime * enemySpeed);
            if (currentCoordinates == end)
            {
            isMovingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            currentCoordinates = Vector2.MoveTowards(currentCoordinates, start, Time.deltaTime * enemySpeed);
            if (currentCoordinates == start)
            {
            isMovingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        transform.position = currentCoordinates;
    }
}
