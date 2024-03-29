using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public PlayerLife playerLife;
    public Rigidbody2D rb;
    public SpriteRenderer rbSprite; //Flip left when moving back
    public Animator anim; //Trigger animation
    public LayerMask groundLayer;
    private BoxCollider2D rbCollider; //GroundCheckTransform for rb

    [Header("Movement")]
    public bool canMove;
    private float moveInput; //Horizontal Movement
    public float moveSpeed = 10f;
    public float acceleration = 13f;
    public float deceleration = 25f;
    public float velPower = 1f;
    public float frictionAmount = 1f;

    [Header("Jump")]
    public bool canJump;
    public bool isJumping;
    public float jumpForce = 15f;
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private float jumpCutMultiplier = 0.5f;
    private float originalGravity = 3f;
    public float fallGravityMultiplier = 1.5f;
    public float maxFallSpeed = 30f;
    public bool externalForce;

    [Header("Dash")]
    public bool canDash; //in general
    public bool ableToDash = true; //for in game dashing
    public Image dashCooldownBorder;
    public Image dashCooldownIcon;
    private bool isDashing;
    private bool isDashingCooldown;
    private float dashingPower = 25f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1.5f;
    private bool isCooldown = false;

    [Header("Debuffs")]
    public bool checkSlow; //Slow Debuff Toggle
    public float slowTimer; //Slow Debuff Timer
    public float knockForceX;
    public float knockForceY;
    public float knockCounter;
    public float knockTotalTime;
    private bool knockedHorizontal; //All traps
    public bool knockedRight;
    private bool knockedVerticalUp; //Horizonal laser
    public bool knockedTopRight;
    private bool knockedVerticalDown; //Horizontal laser
    public bool knockedBottomRight;
    private bool knocked = false; //Stop dash after being knocked

    [Header("Other Settings")]
    public ParticleSystem dashEffect; //dash particle system
    public CameraControl cameraControl;
    public PauseMenu pauseMenu;

    private enum movementState { idle, running, jumping, falling } //Array 0, 1, 2, 3

    private void Awake()
    {
        dashEffect.Stop();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rbSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rbCollider = GetComponent<BoxCollider2D>();
        dashCooldownBorder.enabled = false;
        dashCooldownIcon.fillAmount = 0;
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(12, 8);
    }

    // Update is called once per frame
    private void Update()
    {
        if (pauseMenu.pauseScreen.activeSelf || pauseMenu.controlsScreen.activeSelf || pauseMenu.soundScreen.activeSelf ||
            pauseMenu.isDialogueActive)
        {
            canMove = false;
            canJump = false;
            canDash = false;

            anim.enabled = false;
            if (!isDashing && playerLife.currentHealth > 0)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0f;
            }
        }
        else if (playerLife.currentHealth <= 0)
        {
            canMove = false;
            canJump = false;
            canDash = false;
        }
        else
        {
            canMove = true;
            canJump = true;
            canDash = true;
            anim.enabled = true;
            if (!isDashing)
            {
                rb.gravityScale = originalGravity;
            }
        }

        if (!canMove && !canJump && !canDash)
        {
            return;
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

            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                jumpBufferCounter = 0f;

                AudioManager.Instance.PlayEffectsOneShot("Jump");

                StartCoroutine(JumpCooldown());
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);

                coyoteTimeCounter = 0f;
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

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (ableToDash)
                {
                    StartCoroutine(Dash());
                    if (isCooldown == false)
                    {
                        isCooldown = true;
                        dashCooldownIcon.fillAmount = 1;
                    }
                }
            }

            if (!isDashingCooldown)
            {
                ableToDash = true;
            }

            if (isCooldown)
            {
                dashCooldownBorder.enabled = true;
                dashCooldownIcon.fillAmount -= 1 / dashingCooldown * Time.deltaTime;
                if (dashCooldownIcon.fillAmount <= 0)
                {
                    dashCooldownBorder.enabled = false;
                    dashCooldownIcon.fillAmount = 0;
                    isCooldown = false;
                }
            }

            CheckSlowed();

            if (knocked == true) //If knockbacked, cannot dash for awhile
            {
                StartCoroutine(PauseDash());
            }

            UpdateAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (!canMove && !canJump && !canDash)
        {
            return;
        }
        else
        {
            if (isDashing)
            {
                return;
            }

            if (knockCounter <= 0f)
            {
                if (!externalForce)
                {
                    //Calculate direction player moves in and the desired velocity
                    float targetSpeed = moveInput * moveSpeed;
                    //Calculate difference between current velocity and desired velocity
                    float speedDif = targetSpeed - rb.velocity.x;
                    //Change acceleration rate depending on situation
                    float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? (50 * acceleration / moveSpeed) : (50 * deceleration / moveSpeed);
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
                }
            }
            else
            {
                if (playerLife.currentHealth > 0)
                {
                    CheckKnockback();
                }
            }
        }
    }

    public bool IsGrounded() //Check whether is grounded to prevent infinite jumps
    {
        return Physics2D.BoxCast(rbCollider.bounds.center, rbCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer); //center, size, angle, direction, distance, layer - Returns boolean by itself
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    public void SetExternalForce(bool value) //Knockback
    {
        externalForce = value;
    }

    private IEnumerator Dash()
    {
        ableToDash = false;
        isDashing = true;
        isDashingCooldown = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        externalForce = false; // for jump pads
        Vector2 direction = new Vector2(moveInput, 0f);

        if (rbSprite.flipX == true) //faced left
        {
            direction = new Vector2(-dashingPower, 0f);
            dashEffect.transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
        else if (rbSprite.flipX == false) //faced right
        {
            direction = new Vector2(dashingPower, 0f);
            dashEffect.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }

        rb.velocity = direction.normalized * dashingPower;
        dashEffect.Play();
        anim.SetTrigger("dashing");
        AudioManager.Instance.PlayEffectsOneShot("Dash");
        StartCoroutine(cameraControl.ScreenShake());
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(3, 8, true);

        yield return new WaitForSeconds(dashingTime);

        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(3, 8, false);

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        rb.gravityScale = originalGravity;
        isDashing = false;

        float coolingDown = dashingCooldown;
        while (coolingDown > 0f)
        {
            while (playerLife.currentHealth <= 0 || pauseMenu.isDialogueActive)
            {
                dashEffect.Pause();
                yield return null;
                dashEffect.Play();
            }
            coolingDown -= Time.deltaTime;
            yield return null;
        }

        ableToDash = true;
        isDashingCooldown = false;
    }

    public void CheckSlowed()
    {
        if (checkSlow == true) //Slow debuff timer
        {
            if (playerLife.currentHealth <= 0)
            {
                checkSlow = false;
            }
            else
            {
                moveSpeed = 6f;

                if (isDashing)
                {
                    moveSpeed = 10f;
                    checkSlow = false;
                }
                else
                {
                    slowTimer += Time.deltaTime;
                    if (slowTimer >= 1f) //Similar debuff time to GetHurt()
                    {
                        moveSpeed = 10f;
                        slowTimer = 0f;
                        checkSlow = false;
                    }
                }
            }
        }
    }

    public void CheckKnockback()
    {
        knocked = true;
        if (knockedHorizontal == true)
        {
            if (knockedRight == true)
            {
                rb.velocity = new Vector2(knockForceX, 0);
            }
            else if (knockedRight == false)
            {
                rb.velocity = new Vector2(-knockForceX, 0);
            }
        }

        if (knockedVerticalUp == true)
        {
            if (knockedTopRight == true)
            {
                rb.velocity = new Vector2(knockForceX, knockForceY);
            }
            else if (knockedTopRight == false)
            {
                rb.velocity = new Vector2(-knockForceX, knockForceY);
            }
        }

        if (knockedVerticalDown == true)
        { 
            if (knockedBottomRight == true)
            {
                rb.velocity = new Vector2(knockForceX, -knockForceY);
            }
            else if (knockedBottomRight == false)
            {
                rb.velocity = new Vector2(-knockForceX, -knockForceY);
            }
        }

        knockCounter -= Time.deltaTime;
    }

    public IEnumerator PauseDash()
    {
        ableToDash = false;
        knocked = true;
        yield return new WaitForSeconds(0.6f);
        knocked = false;
    }

    public void UpdateAnimation()
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
        if (rb.velocity.y > 0.1f) //Jump
        {
            state = movementState.jumping;
        }
        else if (rb.velocity.y < -0.1f || !IsGrounded()) //Fall, including apex of jump
        {
            state = movementState.falling;
        }

        anim.SetInteger("state", (int)state); //Animation states at movementState line
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Falling Object"))
        {
            knockedHorizontal = true;
            knockedVerticalUp = false;
            knockedVerticalDown = false;
        }

        if (collision.gameObject.CompareTag("Laser (H)"))
        {
            if (collision.transform.position.y <= transform.position.y)
            {
                knockedHorizontal = false;
                knockedVerticalUp = true;
                knockedVerticalDown = false;
            }
            else if (collision.transform.position.y >= transform.position.y)
            {
                knockedHorizontal = false;
                knockedVerticalUp = false;
                knockedVerticalDown = true;
            }
        }
    }
}
