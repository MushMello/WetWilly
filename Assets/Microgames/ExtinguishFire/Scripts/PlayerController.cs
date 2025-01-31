using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics;
using static UnityEngine.GraphicsBuffer;

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
    private int localScore = 0;

    bool canMove = true;

    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;

    public bool useStopwatch = true;
    public bool lockY = false;
    Collider2D selfCollider;

    // Start is called before the first frame update
    void Start()
    {
        movementInput = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();

        //attack.action.started += ctx => StartInhale();
        //attack.action.canceled += ctx => StartAttack();

        stopwatch = new Stopwatch();
        
    }

    private int LocalScore
    {
        get
        {
            return localScore;
        }
        set
        {
            localScore = value;
            StateHandler state = StateHandler.GetStateHandler();
            if(state)
            {
                state.DisplayScoreValue(localScore);
            }
        }
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

        if(useStopwatch && stopwatch.ElapsedMilliseconds > 3000)
        {
            StartAttack();
        }
    }

    private void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        if(attack.action.WasPressedThisFrame())
        {
            StartInhale();
        }
        if(attack.action.WasReleasedThisFrame())
        {
            StartAttack();
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (lockY && direction.y != 0) {
            direction.y = 0;
        }

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
            isInhaling = false;
            UnityEngine.Debug.Log("StartAttack triggered");
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 3000) {
                attackTime = 3f;
            } else {
                attackTime = stopwatch.ElapsedMilliseconds / 1000f;
            }
            
            animator.SetTrigger("water");
            LockMovement();
            GameObject water = transform.Find("Water").gameObject;
            if(spriteRenderer.flipX)
            {
                water.transform.localPosition = new Vector3(Mathf.Abs(water.transform.localPosition.x) * -1, 0, 0);
            }
            else
            {
                water.transform.localPosition = new Vector3(Mathf.Abs(water.transform.localPosition.x), 0, 0);
            }
            water.SetActive(true);


            float mult = 1f;
            if(spriteRenderer.flipX)
            {
                mult = -1f;
            }
            RaycastHit2D rh = Physics2D.Raycast(water.transform.position, transform.right * mult, selfCollider.bounds.size.x * 8);
            if(rh)
            {
                Rigidbody2D rb = rh.rigidbody;
                if(rb)
                {
                    GameObject target = rb.gameObject;
                    if (target.GetComponent<ParticleSystem>())
                    {
                        target.SetActive(false);
                        LocalScore++;
                        if(LocalScore >= 4  )
                        {
                            StateHandler state = StateHandler.GetStateHandler();
                            if(state)
                            {
                                state.Points = localScore;
                                
                                state.DisplayAnnouncementAndWarp(true, GameScene.Overworld, true);
                            }
                        }
                    }
                }
            }
            StartCoroutine(EndAttackAfterDuration(attackTime));
        }
    }

    private IEnumerator EndAttackAfterDuration(float duration)
    {
        
        UnityEngine.Debug.Log("EndAttackAfterDuration triggered");
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("doIdle");
        UnlockMovement();
        transform.Find("Water").gameObject.SetActive(false);
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
