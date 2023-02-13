using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    // Start is called before the first frame update

    [SerializeField] float enemySpeed = 1f;       //HERE are the presets for your enemy! feel free to change any of the values here
    [SerializeField] float startFollowRange = 5f; //only in the inspector though because the inspectors values override these values
    [SerializeField] float damageRange = 0.5f;
    [SerializeField] float damageAgainDelay = 1f;

    float distance = 0f; //These check don't need to be changed anywhere cause its set by code
    bool damagePlayerCalled = false;
    bool damagedPlayer = false;

    Collider2D colliderType;
    GameObject player;
    Coroutine playerTracker;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        colliderType = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");  //If you want to find by Tag this is what you do
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight() == true)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }


        CheckForPlayer(); //We check if player is in range every frame


    }

    private void CheckForPlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //calculate the distance

        if (distance < startFollowRange) //Check if player is in following range
        {
            MoveToPlayer();
        }
    }
    private IEnumerator DamagePlayer()
    {
        damagePlayerCalled = true;

        if (damagedPlayer)
        {
            yield return new WaitForSeconds(damageAgainDelay); damagedPlayer = false;
        } //If first time getting in range damage immediately

        //Debug.Log("Damage player!"); //You can put whatever you want here
        damagedPlayer = true;
        damagePlayerCalled = false;
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime); //simply moves from our position to player position

        if (distance < damageRange && !damagePlayerCalled) //We say "&&" to say "and" in an if statement, and i check for if damagePlayerCalled == true
        {                                                  //so we don't call the coroutine multiple times
            StartCoroutine(DamagePlayer());
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon; //checking for an extremely small x value if x-value is > 0.0001 it is right < 0.001 is left
    }

    //void OnTriggerEnter2D(Collider2D colliderType)
    //{
    //    if (colliderType.IsTouchingLayers(LayerMask.GetMask("Ground")))
    //    {
    //        StopCoroutine(MoveToPlayer());
    //    }
    //}
}