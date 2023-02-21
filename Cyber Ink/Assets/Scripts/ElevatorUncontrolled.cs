using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUncontrolled : MonoBehaviour
{
    [Header("Positions")]
    //public bool onElevator;
    public Transform startPosition;
    public Transform endPosition;

    [Header("Speed")]
    public float speed;
    private bool moveToEndPos;

    //[Header("Others")]
    //public Rigidbody2D rb;
    //public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        StartElevator();

        //if (onElevator == true)
        //{
        //    if (playerMovement.moveInput != 0f || playerMovement.isDashing == true)
        //    {
        //        rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        //    }
        //    else
        //    {
        //        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        //    }
        //}
    }

    private void StartElevator()
    {
        if (transform.position == startPosition.position)
        {
            moveToEndPos = true;
        }

        if (transform.position == endPosition.position)
        {
            moveToEndPos = false;
        }

        if (moveToEndPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y) //Check if player lands on platform from the top
        {
            collision.transform.SetParent(transform);
            //onElevator = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        //onElevator = false;
        //rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
    }
}
