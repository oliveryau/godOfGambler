using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraControl cameraControl;
    private Rigidbody2D rb;
    private BoxCollider2D rbColl; //GroundCheckTransform for rb
    public SpriteRenderer rbSprite; //Flip left when moving back
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private LayerMask groundLayer;
    private Animator anim; //Trigger animation

    [Header("Movement")]
    public bool canMove = true;
    private float moveInput; //Horizontal Movement
    [SerializeField] private float moveSpeed = 11f;
    [SerializeField] private float acceleration = 13f;
    [SerializeField] private float deceleration = 13f;
    [SerializeField] private float velPower = 1f;
    [SerializeField] private float frictionAmount = 0.6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 15f;
    private float coyoteTimeCounter;
    private float jumpCutMultiplier = 0.5f;
    private float originalGravity = 3f;
    public float fallGravityMultiplier = 2f;
    public float maxFallSpeed = 40f;
    public float coyoteTime = 0.15f;

    private bool canDash = true;
    private bool isDashing;
    private bool isDashingCooldown;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.1f;

    //private float slowTimer; //Slow Debuff Timer
    //private bool checkSlow; //Slow Debuff Toggle

    private enum movementState { idle, running, jumping, falling } //Like array 0,1,2,3

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbColl = GetComponent<BoxCollider2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(12, 8);

    }

    // Update is called once per frame
    private void Update()
    {
        if (!canMove)
        {
            
        } 
        else
        {
            //Prevent player from doing any other action while dashing
            if (isDashing)
            {
                return;
            }

            //Register movement
            moveInput = Input.GetAxis("Horizontal"); //Horizontal movement with input Manager - Horizontal

            //Late jump smoothness
            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            //Friction
            if (IsGrounded())
            {
                if (rbSprite.flipX == true && moveInput > -0.01f)
                {
                    //Use either friction amount (~ 0.2) or 
                    float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
                    //Sets to movement direction
                    amount *= Mathf.Sign(rb.velocity.x);
                    //Applies force against movement direction
                    rb.AddForce(Vector2.left * amount, ForceMode2D.Impulse);
                }
                else if (rbSprite.flipX == false && moveInput < 0.01f)
                {
                    //Use either friction amount (~ 0.2) or 
                    float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
                    //Sets to movement direction
                    amount *= Mathf.Sign(rb.velocity.x);
                    //Applies force against movement direction
                    rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
                }
            }

            if (Input.GetButtonDown("Jump")) //Jump with GetButtonDown in Input Manager
            {
                if (coyoteTimeCounter > 0f)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                if (rb.velocity.y > 0f)
                {
                    rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
                }
            }

            //Setting gravity for fall speed
            if (rb.velocity.y < 0f)
            {
                rb.gravityScale = originalGravity * fallGravityMultiplier;
                //Capping max fall speed, so when falling large distances, it is not too fast
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
            }
            else
            {
                rb.gravityScale = originalGravity;
            }

            if (Input.GetKeyDown(KeyCode.S) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (IsGrounded() && !isDashingCooldown)
            {
                canDash = true;
            }

            //if (checkSlow) //Slow debuff timer
            //{
            //    moveSpeed = 3f;
            //    slowTimer += Time.deltaTime;
            //    if (slowTimer >= 3) //3 second buff
            //    {
            //        moveSpeed = 6f;
            //        slowTimer = 0f;
            //        checkSlow = false;
            //    }
            //}
            UpdateAnimation();

            
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {

        }
        else
        {
            if (isDashing)
            {
                return;
            }

            #region Run
            //Calculate direction player moves in and the desired velocity
            float targetSpeed = moveInput * moveSpeed;
            //Calculate difference between current velocity and desired velocity
            float speedDif = targetSpeed - rb.velocity.x;
            //Change acceleration rate depending on situation
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            //Applies acceleration to speed difference, raises to a set power so acceleration increases with higher speeds
            //Finally multiplies by sign to reapply direction
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            //Applies force to rigidbody, multiplying by Vector2.right so it only affects X axis
            if (moveInput < 0f)
            {
                rb.AddForce(-movement * Vector2.left, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
            }
            #endregion
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //if (collision.gameObject.CompareTag("Slows")) //Check Slows tag
    //{
    //    checkBoost = false;
    //    checkSlow = true; //For speed debuff timer
    //}

    //if (collision.gameObject.CompareTag("Collectible")) //Check Collectible tag
    //{
    //    checkSlow = false;
    //    checkBoost = true; //For speed boost timer
    //}
    //}

    private void UpdateAnimation()
    {
        movementState state;

        if (moveInput > 0f) //Running - Right/positive direction
        {
            state = movementState.running; //movementState line, go to run
            rbSprite.flipX = false; //Flip back to turn right when going forward
        }
        else if (moveInput < 0f) //Left/negative direction
        {
            state = movementState.running;
            rbSprite.flipX = true; //Turn left when going backwards
        }
        else
        {
            state = movementState.idle;
        }

        // Jump and fall here after movement as jump takes higher priority over run
        if (rb.velocity.y > .1f) //Jump
        {
            state = movementState.jumping;
        }
        else if (rb.velocity.y < -.1f) //Fall
        {
            state = movementState.falling;
        }

        anim.SetInteger("state", (int)state); //Animation states at movementState line
    }

    public bool IsGrounded() //Check whether is grounded to prevent infinite jumps
    {
        return Physics2D.BoxCast(rbColl.bounds.center, rbColl.bounds.size, 0f, Vector2.down, .1f, groundLayer); //center, size, angle, direction, distance, layer - Returns boolean by itself
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        isDashingCooldown = true;
        float dashGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 direction = new Vector2(moveInput, 0f);
        if (rbSprite.flipX == true)
        {
            direction = new Vector2(-dashingPower, 0f);
        }
        else if (rbSprite.flipX == false)
        {
            direction = new Vector2(dashingPower, 0f);
        }

        rb.velocity = direction.normalized * dashingPower;
        StartCoroutine(cameraControl.ScreenShake());
        trail.emitting = true;
        Physics2D.IgnoreLayerCollision(7, 8);
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        trail.emitting = false;
        rb.gravityScale = dashGravity;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(7, 8, false);
        yield return new WaitForSeconds(dashingCooldown);
        isDashingCooldown = false;
    }
}
