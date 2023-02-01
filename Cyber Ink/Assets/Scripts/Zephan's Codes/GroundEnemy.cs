using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float storeSpeed; //for restarting the patrol() sequence
    //[SerializeField]
    //private EnemyData data;
    //[SerializeField]
    //private Text statusText;  
    //[SerializeField]
    //private float waitTime = 0.3f;

    private GameObject player;
    //public Transform pathHolder;
    Transform playerlocation;

    //public Light spotlight;
    //public float viewDistance;
    //public LayerMask viewMask;
    //float viewAngle;

    Rigidbody2D myRigidbody;
    [SerializeField]
    Collider2D colliderType;
    [SerializeField]
    Collider2D frontCast;
    //[SerializeField]
    //Collider2D playerCollider;
    [SerializeField]
    private Transform enemy;

    //Coroutine playerTracker;

    [Header("Player Detection")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    public Collider2D rearDetection;
    public float agroRange;
    private bool movingRight = true;
    bool ChasingPlayer = false;
    bool withinRange;//able to use function but this way it is more robust

    [Header("Attack Range")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerLife playerHealth;



    //[Header("Dash")]
    //public bool canDash = true;
    //private bool isDashing;
    //private bool isDashingCooldown;
    //private float dashingPower = 30f;
    //private float dashingTime = 0.2f;
    //private float dashingCooldown = 0.1f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerlocation = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        //float distanceToplayer;
        //RaycastHit2D front = Physics2D.Linecast(transform.position, player.transform.position);

        if (colliderType.IsTouchingLayers(LayerMask.GetMask("Waypoint Collider")))
        {
            FlipEnemy();
            Debug.Log("collided");
        }
        else if (PlayerInSight())
        {
            ChasingPlayer = true;
            MoveToPlayer();
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                DamagePlayer();
            }
        }

        //}
        //else if (front.collider.tag == "Player")
        //{
        //    ChasingPlayer = true;
        //    MoveToPlayer();
        //}
        else
        {
            speed = storeSpeed;
            Patrol();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private void DamagePlayer()
    {
        //If player is in range, damage
        if (PlayerInSight())    
        {
            playerHealth.TakeDamage(damage);
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<PlayerLife>();

        return hit.collider != null;
    }


    private void MoveToPlayer()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(transform.position.x < player.transform.position.x)
        {
            myRigidbody.velocity = new Vector2(speed * Time.deltaTime, 0);
        }

        else 
        {
            myRigidbody.velocity = new Vector2(-speed * Time.deltaTime, 0);
        }
    }

    private void Patrol()
    {
        if (speed != 0)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.right * storeSpeed * Time.deltaTime);
    }
    private void FlipEnemy()
    {
        //Debug.Log("Flip");
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }

        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Player") && ChasingPlayer == true)
        {

            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                MoveToPlayer();
            }

            else
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                MoveToPlayer();
            }
        }


    }

    //private IEnumerator Dash()
    //{
    //    canDash = false;
    //    isDashing = true;
    //    isDashingCooldown = true;
    //    float dashGravity = myRigidbody.gravityScale;
    //    myRigidbody.gravityScale = 0f;

    //    Vector2 direction = new Vector2(moveInput, 0f);
    //    if (rbSprite.flipX == true)
    //    {
    //        direction = new Vector2(-dashingPower, 0f);
    //    }
    //    else if (rbSprite.flipX == false)
    //    {
    //        direction = new Vector2(dashingPower, 0f);
    //    }

    //    rb.velocity = direction.normalized * dashingPower;
    //    anim.SetTrigger("attack");
    //    Attack();
    //    StartCoroutine(cameraControl.ScreenShake());
    //    trail.emitting = true;
    //    Physics2D.IgnoreLayerCollision(7, 8);
    //    yield return new WaitForSeconds(dashingTime);
    //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    //    trail.emitting = false;
    //    rb.gravityScale = dashGravity;
    //    isDashing = false;
    //    Physics2D.IgnoreLayerCollision(7, 8, false);
    //    yield return new WaitForSeconds(dashingCooldown);
    //    isDashingCooldown = false;
    //}



    // to stop the enemy when player is within range need to add new attackrange gameobject
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    withinRange = true;
    //    if ((collision.gameObject.CompareTag("Player")))
    //    {
    //        Physics2D.IgnoreLayerCollision(3, 8);
    //        speed = 0; // stop chasing player
    //        DamagePlayer();

    //    }

    //}

    //private void OnCollisionExit2D(Collision2D collision) // need to edit this
    //{
    //    withinRange = false;
    //    if(withinRange == false)
    //    {
    //        speed = storeSpeed;
    //        transform.Translate(Vector2.right * speed * Time.deltaTime);
    //        Physics2D.IgnoreLayerCollision(3, 8, false);
    //    }
    //}


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (rearDetection.IsTouchingLayers(LayerMask.GetMask("Player")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
    //    {
    //        ChasingPlayer = true;
    //    }

    //    else
    //    {
    //        ChasingPlayer = false;
    //        rearDetection.isTrigger = false;
    //    }
    //}













}