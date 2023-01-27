using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform player;
    public Transform elevatorSwitch;
    public Transform downPos;
    public Transform upperPos;
    public SpriteRenderer elevator;

    public float speed = 3f;
    public bool movingElevatorUp;

    // Update is called once per frame
    void Update()
    {
        StartElevator();
        DisplayColor();
    }

    private void StartElevator()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position) < 1f) //If player stays on elevator, elevator will move up
        {
            movingElevatorUp = true;
        }

        if (Vector2.Distance(player.position, elevatorSwitch.position) > 1f) //If player leaves elevator, elevator will go back to original state
        {
            movingElevatorUp = false;
        }

        if (movingElevatorUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, upperPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, downPos.position, speed * Time.deltaTime);
        }
    }

    private void DisplayColor()
    {
        if (transform.position.y <= downPos.position.y || transform.position.y >= upperPos.position.y)
        {
            elevator.color = Color.green;
        }
        else
        {
            elevator.color = Color.red;
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
