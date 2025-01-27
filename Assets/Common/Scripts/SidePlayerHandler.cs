using UnityEngine;
using UnityEngine.InputSystem;

// SidePlayerHandler
// base class for side-facing player movement, extend to implement additional functionality

public class SidePlayerHandler : MonoBehaviour
{
    private const float internalSpeedMult = 1000f; // internally multiplied with our player's speed in update()
                                                   // to overcome time.deltatime making the player verrrry slow

    [Header("Player Controls")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool spriteRightFacing = true;
    [SerializeField] private bool handleSpriteFlip = true;
    [Header("References")]
    [SerializeField] private InputActionReference movement;

    private Vector2 pendingVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private bool locked = false;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public Vector2 GetInputVector()
    {
        return movement.action.ReadValue<Vector2>();
    }

    protected virtual void Update()
    {
        Vector2 inputVector = GetInputVector();
        if (!locked && rb && inputVector != Vector2.zero)
        {
            pendingVelocity += (inputVector * speed * Time.deltaTime * internalSpeedMult);
            if(spriteRenderer && handleSpriteFlip)
            {
                if(spriteRightFacing)
                {
                    spriteRenderer.flipX = inputVector.x < 0;
                }
                else
                {
                    spriteRenderer.flipX = inputVector.x > 0;
                }
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!locked && pendingVelocity != Vector2.zero)
        {
            rb.AddForce(pendingVelocity);
            pendingVelocity = Vector2.zero;
        }
    }

    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
}
