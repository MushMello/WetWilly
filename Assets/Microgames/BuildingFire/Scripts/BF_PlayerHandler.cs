using UnityEngine;
using UnityEngine.InputSystem;

// BF_PlayerHandler
// handles the player movement in Building Fire

public class BF_PlayerHandler : MonoBehaviour
{
    private const float internalSpeedMult = 1000f; // internally multiplied with our player's speed in update()
                                                  // to overcome time.deltatime making the player verrrry slow

    [Header("Player Controls")]
    [SerializeField] private float speed = 1f;
    [Header("References")]
    [SerializeField] private BF_GameHandler gameHandler;
    [SerializeField] private InputActionReference movement;

    private Vector2 pendingVelocity = Vector2.zero;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetInputVector()
    {
        if(gameHandler && gameHandler.Running)
        {
            return movement.action.ReadValue<Vector2>();
        }
        return Vector2.zero;
    }

    private void Update()
    {
        Vector2 inputVector = GetInputVector();
        if (rb && inputVector != Vector2.zero)
        {
            pendingVelocity += (inputVector * speed * Time.deltaTime * internalSpeedMult);
        }
    }

    private void FixedUpdate()
    {
        if(pendingVelocity != Vector2.zero)
        {
            rb.AddForce(pendingVelocity);
            pendingVelocity = Vector2.zero;
        }
    }

    public void RegisterSave()
    {
        if(gameHandler)
        {
            gameHandler.Points++;
        }
    }
}
