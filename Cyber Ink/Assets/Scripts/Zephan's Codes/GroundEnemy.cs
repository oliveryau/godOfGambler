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
    [SerializeField] private BoxCollider2D playerDetection;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform castPoint;
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
    private PlayerMovement playerMovement;


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
        if (colliderType.IsTouchingLayers(LayerMask.GetMask("Waypoint Collider")))
        {
            FlipEnemy();
            //Debug.Log("collided");
        }
        else if (CanSeePlayer(agroRange) == true)
        {
            //Debug.Log("chasing");
            ChasingPlayer = true;
            MoveToPlayer();
        }
        else
        {
            speed = storeSpeed;
            Patrol();
        }

    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        Vector2 endPos = castPoint.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                // chase the enemy
                val = true;
                
            }

            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val;
    }
    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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



  












}