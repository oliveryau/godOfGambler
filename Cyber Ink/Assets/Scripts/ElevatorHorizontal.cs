using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorHorizontal : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    public bool moveToEndPos;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        StartElevator();
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
