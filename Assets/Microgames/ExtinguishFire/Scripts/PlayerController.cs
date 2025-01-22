using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{

    public float attackTime = 1f;
    private bool isInhaling = false;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public AquaAttack aquaAttack;

    Vector2 movementInput;

    SpriteRenderer spriteRenderer;

    Stopwatch stopwatch;

    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;

    bool canMove = true;

    [SerializeField]
    private InputActionReference movement, attack;

    // Start is called before the first frame update
    void Start()
    {
        movementInput = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        attack.action.started += ctx => StartInhale();
        attack.action.canceled += ctx => StartAttack();

        stopwatch = new Stopwatch();
        
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } 
        }
        return false;
    }

    void StartInhale()
    {
        if (!isInhaling)
        {
            UnityEngine.Debug.Log("StartInhale triggered");
            stopwatch.Reset();
            stopwatch.Start();
            isInhaling = true;
            animator.SetTrigger("inhale");
        }
    }

    void StartAttack()
    {
        if (isInhaling)
        {
            UnityEngine.Debug.Log("StartAttack triggered");
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 3000) {
                attackTime = 3f;
            } else {
                attackTime = stopwatch.ElapsedMilliseconds / 1000f;
            }

            isInhaling = false;
            animator.SetTrigger("water");
            LockMovement();

            StartCoroutine(EndAttackAfterDuration(attackTime));
        }
    }

    private IEnumerator EndAttackAfterDuration(float duration)
    {
        
        UnityEngine.Debug.Log("EndAttackAfterDuration triggered");
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("doIdle");
        UnlockMovement();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
