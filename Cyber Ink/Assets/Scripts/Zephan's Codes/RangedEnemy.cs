using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
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



    [Header("Attack Parameters")]
    private float shotCooldown;
    public float startShotCooldown;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    public Transform firepoint;

    public GameObject bullet;

    private Animator anim;
    private PlayerLife playerHealth;
    private PlayerMovement playerMovement;

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
        //float distanceToplayer; 
        //RaycastHit2D front = Physics2D.Linecast(transform.position, player.transform.position); 
        //RaycastHit2D front = Physics2D.Raycast(transform.position, Vector2.right, agroRange); 
        if (colliderType.IsTouchingLayers(LayerMask.GetMask("Waypoint Collider")))
        {
            FlipEnemy();
            //Debug.Log("collided"); 
        }
        //else if (PlayerInSight()) 
        //{ 
        //    ChasingPlayer = true; 
        //    MoveToPlayer(); 
        //    //if (cooldownTimer >= attackCooldown) 
        //    //{ 
        //    //    //Attack 
        //    //    cooldownTimer = 0; 
        //    //    anim.SetTrigger("attack"); 
        //    //    DamagePlayer(); 
        //    //} 
        //} 

        //} 
        else if (CanSeePlayer(agroRange) == true)
        {
            //Debug.Log("chasing"); 
            ChasingPlayer = true;
            MoveToPlayer();

            if (shotCooldown <= 0)
            {
                //Attack
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                Debug.Log("Shooting");
                shotCooldown = startShotCooldown;
                //anim.SetTrigger("attack");
            }
            else
                shotCooldown -= Time.deltaTime;

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
        Vector2 endPos = castPoint.position + castPoint.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
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

//private void OnDrawGizmos()
//{
//    Gizmos.color = Color.cyan;
//    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
//        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
//}
//private void RangedAttack()
//{
//    cooldownTimer = 0;
//    fireballs[FindFireballs()].transform.position = firepoint.position;
//    fireballs[FindFireballs()].GetComponent<EnemyProjectile>().ActivateProjectile(transform.position.x);
//}

//private bool PlayerInSight()
//{
//    RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
//        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

//    if (hit.collider != null)
//        playerHealth = hit.transform.GetComponent<PlayerLife>();

//    return hit.collider != null;
//}

//private int FindFireballs()
//{
//    for(int i =0; i< fireballs.Length; i++)
//    {
//        if (!fireballs[i].activeInHierarchy)
//            return i;
//    }
//    return 0;
//}