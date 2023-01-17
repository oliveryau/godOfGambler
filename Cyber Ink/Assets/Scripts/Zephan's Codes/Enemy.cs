using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //[SerializeField]
    //private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;
    //[SerializeField]
    //private EnemyData data;
    //[SerializeField]
    //private Text statusText;
    //[SerializeField]
    //private float waitTime = 0.3f;

    private GameObject player;
    public Transform pathHolder;
    Transform playerlocation;

    //public Light spotlight;
    //public float viewDistance;
    //public LayerMask viewMask;
    //float viewAngle;

    Rigidbody2D myRigidbody;
    [SerializeField]
    Collider2D colliderType;
    Coroutine playerTracker;

    public Transform groundDetection;
    public float distance;
    private bool movingRight = true;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerlocation = GameObject.FindGameObjectWithTag("Player").transform;

        
        //SetEnemyValues();
        //Vector2[] waypoints = new Vector2[pathHolder.childCount];
        //for(int i = 0; i < waypoints.Length; i++)
        //{
        //    waypoints[i] = pathHolder.GetChild(i).position; 
        //}

        //StartCoroutine(FollowPath(waypoints));
    }

    void Update()
    {
       
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D rayInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
            //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (rayInfo.collider.tag == "Player")
        {
            MoveToPlayer();
        }

        else if (colliderType.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            Patrol();
        }


    }
 
    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Patrol()
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

    //bool CanSeePlayer()
    //{
    //    if (Vector2.Distance(transform.position, playerlocation.position) < viewDistance) // within distance
    //    {
    //        Vector2 dirToPlayer = (playerlocation.position - transform.position).normalized;
    //        float angleBetweenGuardAndPlayer = Vector2.Angle(transform.forward, dirToPlayer);
    //        if (angleBetweenGuardAndPlayer < viewAngle / 2f) //within viewAngle 
    //        {
    //            if (!Physics.Linecast(transform.position, playerlocation.position, viewMask))
    //            {
    //                return true; // all 3 conditions met (within angle, within distance and no object blocking)
    //            }
    //        }
    //    }
    //    return false;
    //}

    //private void SetEnemyValues()
    //{
    //    GetComponent<Health>().SetHealth(data.hp, data.hp);
    //    damage = data.damage;
    //    speed = data.speed;
    //}

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