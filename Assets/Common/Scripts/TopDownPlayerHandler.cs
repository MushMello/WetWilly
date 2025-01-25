using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerHandler : MonoBehaviour
{
    private const float internalSpeedMult = 1000f; // internal const to offset time.deltaTime slowdown

    [Header("Player Controls")]
    [SerializeField] private float speed = 1f;
    [Header("References")]
    [SerializeField] private InputActionReference movement;

    private Rigidbody2D rb;
    private bool locked = false;
    private Vector2 pendingVelocity = Vector2.zero;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (!locked)
        {
            Vector2 inputVector = GetInputVector();
            pendingVelocity += (inputVector * speed * Time.deltaTime * internalSpeedMult);
            if(inputVector != Vector2.zero)
            {
                transform.up = new Vector3(inputVector.x, inputVector.y, 0f);
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if(rb && pendingVelocity != Vector2.zero)
        {
            rb.AddForce(pendingVelocity);
            pendingVelocity = Vector2.zero;
        }
    }

    public Vector2 GetInputVector()
    {
        if(movement)
        {
            return movement.action.ReadValue<Vector2>();
        }
        return Vector2.zero;
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
}
