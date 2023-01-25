using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //[SerializeField]
    //private int damage = 5;
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
    public Transform frontDetection;
    public Collider2D rearDetection;
    public float agroRange;
    private bool movingRight = true;
    bool ChasingPlayer = false;
    bool withinRange;//able to use function but this way it is more robust


    bool damagePlayerCalled = false;
    bool damagedPlayer = false;
    [Header("Damage")]
    [SerializeField] float damageRange = 0.5f;
    [SerializeField] float damageAgainDelay = 1f;
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerlocation = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        float distanceToplayer;
        RaycastHit2D hitInfo;
        //RaycastHit2D front = Physics2D.Raycast(frontDetection.position, Vector2.right, (int)agroRange);
        RaycastHit2D front = Physics2D.Linecast(transform.position, player.transform.position);

        //if(Physics.Linecast(transform.position, player.transform.position, out hitInfo, 1 << LayerMask.NameToLayer("Ground")))
        //    {

        //}
        if (colliderType.IsTouchingLayers(LayerMask.GetMask("Waypoint Collider")))
        {
            FlipEnemy();
            Debug.Log("collided");
        }
        else if (movingRight == true)
        {
            if (front.distance <= agroRange)
                speed = 0;
            else
            {
                speed = storeSpeed;
                Patrol();
            }
                
        }
        else if (movingRight == false)
        {
            if (front.distance >= agroRange) //need to check this 
                speed = 0;
            else
            {
                speed = storeSpeed;
                Patrol();
            }
                
        }
        else if (front.collider.tag == "Player")
        {
            ChasingPlayer = true;
            MoveToPlayer();
        }
        else
        {
            speed = storeSpeed;
            Patrol();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * agroRange);
    }

    //IEnumerator StopChasingPlayer() //can use for hearts too
    //{ 
    //    Physics2D.IgnoreLayerCollision(3, 8);
    //    myRigidbody.velocity = new Vector2(0, 0);
    //    yield return new WaitForSeconds(3);
    //    Physics2D.IgnoreLayerCollision(3, 8, false);
    //}

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
        Debug.Log("Flip");
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
    private IEnumerator DamagePlayer()
    {
        damagePlayerCalled = true;

        if (damagedPlayer)
        {
            yield return new WaitForSeconds(damageAgainDelay); damagedPlayer = false;
        } //If first time getting in range damage immediately

        Debug.Log("Damage player!"); //You can put whatever you want here
        damagedPlayer = true;
        damagePlayerCalled = false;
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

    // player tracking using raycast

    //RaycastHit2D front = Physics2D.Raycast(frontDetection.position, Vector2.right, agroRange);

    ////RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
    //if (front.collider.tag == "Player")
    //{
    //    MoveToPlayer();
    //}

    //myRigidbody.velocity = new Vector2(speed, 0);
    // distance to player


    //if (transform.position.x + 0.01 == player.transform.position.x )
    //{
    //    StopChasingPlayer();
    //    speed = 0;
    //}

    //float distanceToplayer = Vector2.Distance(transform.position, player.transform.position);
    //float verticalDistance = Math.Abs(transform.position.y - player.transform.position.y);
    //for player tracking based on vector2.distance
    //else if (distanceToplayer < agroRange || playerCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
    //{
    //    MoveToPlayer();
    //}

    //private void SetEnemyValues()
    //{
    //    GetComponent<Health>().SetHealth(data.hp, data.hp);
    //    damage = data.damage;
    //    speed = data.speed;
    //}


    //place in Start() for waypoints

    //SetEnemyValues();
    //Vector2[] waypoints = new Vector2[pathHolder.childCount];
    //for(int i = 0; i < waypoints.Length; i++)
    //{
    //    waypoints[i] = pathHolder.GetChild(i).position; 
    //}

    //StartCoroutine(FollowPath(waypoints));

    //void OnDrawGizmos()
    //{
    //    Vector2 startPosition = pathHolder.GetChild(0).position;
    //    Vector2 previousPosition = startPosition;
    //    foreach (Transform waypoint in pathHolder)
    //    {
    //        Gizmos.DrawSphere(waypoint.position, 0.3f);
    //        Gizmos.DrawLine(previousPosition, waypoint.position);
    //        previousPosition = waypoint.position;
    //    }
    //    Gizmos.DrawLine(previousPosition, startPosition);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    //}

    //IEnumerator FollowPath (Vector2[] waypoints)
    //{
    //    transform.position = waypoints [0];
    //    int targetWaypointIndex = 1;
    //    Vector2 targetWaypoint = waypoints[targetWaypointIndex];
    //    while(true)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
    //        if((Vector2)transform.position == targetWaypoint)
    //        {
    //            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
    //            targetWaypoint = waypoints [targetWaypointIndex];
    //            yield return new WaitForSeconds(waitTime);

    //        }
    //        yield return null;
    //    }
    //}

    //private bool IsFacingRight()
    //{
    //    return transform.localScale.x > Mathf.Epsilon; //checking for an extremely small x value if x-value is > 0.0001 it is right < 0.001 is left
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //Just Turns GameObject
    //    transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y); 

    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //if (collider.CompareTag("Player"))
    //{
    //   collider.GetComponent<Health>().Damage(damage);
    //   this.GetComponent<Health>().Damage(10000);
    //}

    //if (collider.gameObject.CompareTag("Enemy")) //Check Slows tag
    //{
    //    Destroy(collider.gameObject);
    //    statusText.text = "Enemy Defeated!";

    //}
    //}


}