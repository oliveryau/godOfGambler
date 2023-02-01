using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField]
    Collider2D colliderType;
    [SerializeField]
    Collider2D frontCast;
    [SerializeField]
    private float speed;
    public float storeSpeed;
    private GameObject player;
    Rigidbody2D myRigidbody;

    [Header("Player Detection")]
    [SerializeField] private LayerMask playerLayer;
    public Collider2D rearDetection;
    public float agroRange;
    private bool movingRight = true;
    bool ChasingPlayer = false;
    bool withinRange;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;



    private Animator anim;
    private PlayerLife playerHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        float distanceToplayer;
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
                RangedAttack();
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
    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireballs[FindFireballs()].transform.position = firepoint.position;
        fireballs[FindFireballs()].GetComponent<EnemyProjectile>().ActivateProjectile(transform.position.x);
    }

    private int FindFireballs()
    {
        for(int i =0; i< fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
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
        if (transform.position.x < player.transform.position.x)
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
}
