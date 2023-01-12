using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D rbColl; //GroundCheckTransform for rb
    private SpriteRenderer rbSprite; //Flip left when moving back
    private Animator anim; //Trigger animation
    [SerializeField] private TrailRenderer trail1;
    //[SerializeField] private TrailRenderer trail2;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    private float horizontalMovementInput = 0f; //Horizontal Movement
    [SerializeField] private float moveSpeed = 9f; //Move Speed
    [SerializeField] private float acceleration = 13f;
    [SerializeField] private float deceleration = 18f;
    [SerializeField] private float velPower = 0.96f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 13f; //Jump Force
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    public float fallGravityMultiplier = 2f;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private bool isDashingCooldown;
    private float dashingPower = 60f;
    private float dashingTime = 0.2f;
    private float originalGravity;

    //private float speedBoostTimer; //Speed Boost Timer
    //private bool checkBoost; //Speed Boost Toggle
    //private float slowTimer; //Slow Debuff Timer
    //private bool checkSlow; //Slow Debuff Toggle

    private enum movementState { idle, running, jumping, falling } //Like array 0,1,2,3

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Standard stuff
        rbColl = GetComponent<BoxCollider2D>(); //Standard stuff
        rbSprite = GetComponent<SpriteRenderer>(); //Standard stuff
        anim = GetComponent<Animator>(); //Standard stuff

        //speedBoostTimer = 0f;
        //checkBoost = false;
        //slowTimer = 0f;
        //checkSlow = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing) //Prevent player from doing any other action while dashing
        {
            return;
        }

        horizontalMovementInput = Input.GetAxis("Horizontal"); //Horizontal movement with input Manager - Horizontal
        rb.velocity = new Vector2(horizontalMovementInput * moveSpeed, rb.velocity.y); //Negative and positive x values, don't set y to 0

        //Late jump smoothness
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) //Jump with GetButtonDown in Input Manager
        {
            if (coyoteTimeCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            //if (rb.velocity.y < 0)
            //{
            //    rb.gravityScale = originalGravity * fallGravityMultiplier;
            //}
            //else
            //{
            //    rb.gravityScale = originalGravity;
            //}
        }

        if (Input.GetKeyDown(KeyCode.S) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (!isDashingCooldown)
        {
            canDash = true;
            if (IsGrounded())
            {
                canDash = false;
            }
        }

        //if (checkBoost) //Speed boost timer
        //{
        //    moveSpeed = 8.5f;
        //    speedBoostTimer += Time.deltaTime;
        //    if (speedBoostTimer >= 4) //4 second buff
        //    {
        //        moveSpeed = 6f;
        //        speedBoostTimer = 0f;
        //        checkBoost = false;
        //    }
        //}

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

    private void FixedUpdate()
    {
        #region Run
        //Calculate direction player moves in and the desired velocity
        float targetSpeed = horizontalMovementInput * moveSpeed;
        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //Change acceleration rate depending on situation
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        //Applies acceleration to speed difference, raises to a set power so acceleration increases with higher speeds
        //Finally multiplies by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        //Applies force to rigidbody, multiplying by Vector2.right so it only affects X axis
        rb.AddForce(movement * Vector2.right);
        #endregion
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

        if (horizontalMovementInput > 0f) //Running - Right/positive direction
        {
            state = movementState.running; //movementState line, go to run
            rbSprite.flipX = false; //Flip back to turn right when going forward
        }
        else if (horizontalMovementInput < 0f) //Left/negative direction
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
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(horizontalMovementInput * dashingPower, 0f);
        trail1.emitting = true;
        //trail2.emitting = true;
        Physics2D.IgnoreLayerCollision(7, 8);
        yield return new WaitForSeconds(dashingTime);
        trail1.emitting = false;
        //trail2.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(7, 8, false);
        yield return new WaitUntil(() => IsGrounded());
        canDash = true;
        isDashingCooldown = false;
    }
}
