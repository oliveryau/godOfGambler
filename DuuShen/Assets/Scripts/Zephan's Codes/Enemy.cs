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
    [SerializeField]
    private EnemyData data;
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private float waitTime = 0.3f;

    private GameObject player;
    public Transform pathHolder;
    Transform playerlocation;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;


    void Start()
    {
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
        if (CanSeePlayer())
        {
            Swarm();
        }

    }

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



    bool CanSeePlayer()
    {
        if (Vector2.Distance(transform.position, playerlocation.position) < viewDistance) // within distance
        {
            Vector2 dirToPlayer = (playerlocation.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector2.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) //within viewAngle 
            {
                if (!Physics.Linecast(transform.position, playerlocation.position, viewMask))
                {
                    return true; // all 3 conditions met (within angle, within distance and no object blocking)
                }
            }
        }
        return false;
    }

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
    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

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