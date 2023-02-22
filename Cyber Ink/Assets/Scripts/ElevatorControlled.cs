using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControlled : MonoBehaviour
{
    [Header("Positions")]
    public Transform startPosition;
    public Transform endPosition;

    [Header("Speed")]
    public float toSpeed;
    public float backSpeed;
    private bool movingElevator;

    // Update is called once per frame
    void Update()
    {
        StartElevator();
    }

    private void StartElevator()
    {
        if (movingElevator)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.position, toSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, backSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y) //Check if player lands on platform from the top
        {
            movingElevator = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        movingElevator = false;
        collision.transform.SetParent(null);
    }
}
