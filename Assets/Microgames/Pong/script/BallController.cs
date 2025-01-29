using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float movementSpeed = 5f;

    Rigidbody2D rb;

    GameObject controller;

    Vector2 currentDirection;

    public GameObject prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = new Vector2(1,Random.Range(-0.5f,-1f));

        controller = GameObject.Find("GameController");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(currentDirection);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            float hitFactor = (transform.position.x - col.transform.position.x) / col.collider.bounds.size.x;
            currentDirection = new Vector2(hitFactor, 1).normalized;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyAI>().playExplosion();
            WaitForSeconds wait = new WaitForSeconds(0.5f);
            Destroy(col.gameObject);
        }
        else if (col.contacts.Length > 0)
        {
            ContactPoint2D contact = col.contacts[0];
            Vector2 normal = contact.normal;

            if (normal.x != 0)
            {
            currentDirection.x = -currentDirection.x;
            }

            if (normal.y != 0)
            {
            currentDirection.y = -currentDirection.y;
            }
        }
    }

    void OnDestroy() {
        controller.GetComponent<GameController>().BallDestroyed(prefab);
    }

    void Move(Vector2 direction) {
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
}
