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
    private bool isElevatorDown;

    // Update is called once per frame
    void Update()
    {
        StartElevator();
        DisplayColor();
    }

    private void StartElevator()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position) < 1f && Input.GetKeyDown(KeyCode.Z))
        {
            if (transform.position.y <= downPos.position.y)
            {
                isElevatorDown = true;
            }
            else if (transform.position.y >= upperPos.position.y)
            {
                isElevatorDown = false;
            }
        }

        if (isElevatorDown)
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
}
