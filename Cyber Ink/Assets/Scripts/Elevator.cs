using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform player;
    public Transform elevatorSwitch;
    public Transform startPosition;
    public Transform endPosition;
    public SpriteRenderer elevatorSprite;

    public float speed = 5f;
    public bool movingElevator;

    // Update is called once per frame
    void Update()
    {
        StartElevator();
        DisplayColor();
    }

    private void StartElevator()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position) < 0.5f) //If player stays on elevator, elevator will move up
        {
            movingElevator = true;
        }

        if (Vector2.Distance(player.position, elevatorSwitch.position) > 0.5f) //If player leaves elevator, elevator will go back to original state
        {
            movingElevator = false;
        }

        if (movingElevator)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);
        }
    }

    private void DisplayColor()
    {
        if (transform.position.y <= startPosition.position.y || transform.position.y >= endPosition.position.y)
        {
            elevatorSprite.color = Color.green;
        }
        else
        {
            elevatorSprite.color = Color.red;
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
