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

    private GameObject player;
    Transform playerlocation;

    Rigidbody2D myRigidbody;
    [SerializeField]
    Collider2D colliderType;
    [SerializeField]
    Collider2D frontCast;
    [SerializeField]
    private Transform enemy;

    [Header("Player Detection")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D playerDetection;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform castPoint;
    public Collider2D rearDetection;
    public float agroRange;
    private bool movingRight = true;
    bool ChasingPlayer = false;

    [Header("Attack Range")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerLife playerHealth;
    private PlayerMovement playerMovement;

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
        }
        else if (CanSeePlayer(agroRange) == true)
        {
            ChasingPlayer = true;
            MoveToPlayer();
            anim.SetTrigger("attacking");
        }
        else
        {
            speed = storeSpeed;
            Patrol();
            anim.SetTrigger("moving");
        }

    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        Vector2 endPos = castPoint.position + castPoint.right * distance;
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