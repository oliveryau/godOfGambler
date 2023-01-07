using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D rbColl; //GroundCheckTransform for rb
    public SpriteRenderer rbSprite; //Flip left when moving back
    private Animator anim; //Trigger animation
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private LayerMask jumpableGround;

    private float xMove = 0f; //Horizontal Movement
    [SerializeField] float moveSpeed = 6f; //Move Speed
    [SerializeField] private float jumpForce = 12f; //Jump Force

    private bool canDash = true;
    private bool isDashing;
    private bool isDashingCooldown;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    private float originalGravity;

    private float coyoteTime = 0.2f; //Late jump smoothness
    private float coyoteTimeCounter; //Late jump smoothness

    private float speedBoostTimer; //Speed Boost Timer
    private bool checkBoost; //Speed Boost Toggle
    private float slowTimer; //Slow Debuff Timer
    private bool checkSlow; //Slow Debuff Toggle

    private enum movementState { idle, running, jumping, falling } //Like array 0,1,2,3

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Standard stuff
        rbColl = GetComponent<BoxCollider2D>(); //Standard stuff
        rbSprite = GetComponent<SpriteRenderer>(); //Standard stuff
        anim = GetComponent<Animator>(); //Standard stuff
        tr = GetComponent<TrailRenderer>();

        speedBoostTimer = 0f;
        checkBoost = false;
        slowTimer = 0f;
        checkSlow = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing) //Prevent player from doing any other action while dashing
        {
            return;
        }

        xMove = Input.GetAxisRaw("Horizontal"); //Horizontal movement with input Manager - Horizontal
        rb.velocity = new Vector2(xMove * moveSpeed, rb.velocity.y); //Negative and positive x values, don't set y to 0

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

        if (checkBoost) //Speed boost timer
        {
            moveSpeed = 8.5f;
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer >= 4) //4 second buff
            {
                moveSpeed = 6f;
                speedBoostTimer = 0f;
                checkBoost = false;
            }
        }

        if (checkSlow) //Slow debuff timer
        {
            moveSpeed = 3f;
            slowTimer += Time.deltaTime;
            if (slowTimer >= 3) //3 second buff
            {
                moveSpeed = 6f;
                slowTimer = 0f;
                checkSlow = false;
            }
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slows")) //Check Slows tag
        {
            checkBoost = false;
            checkSlow = true; //For speed debuff timer
        }

        if (collision.gameObject.CompareTag("Collectible")) //Check Collectible tag
        {
            checkSlow = false;
            checkBoost = true; //For speed boost timer
        }
    }

    private void UpdateAnimation()
    {
        movementState state;

        if (xMove > 0f) //Running - Right/positive direction
        {
            state = movementState.running; //movementState line, go to run
            rbSprite.flipX = false; //Flip back to turn right when going forward
        }
        else if (xMove < 0f) //Left/negative direction
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

    private bool IsGrounded() //Check whether is grounded to prevent infinite jumps
    {

        return Physics2D.BoxCast(rbColl.bounds.center, rbColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround); //center, size, angle, direction, distance, layer - Returns boolean by itself
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        isDashingCooldown = true;
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(xMove * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        isDashingCooldown = false;
    }
}
