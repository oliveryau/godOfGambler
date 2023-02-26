using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    [Header("Positions")]
    public Transform startPosition;
    public Transform endPosition;

    [Header("Speed")]
    public float speed;
    private bool moveToEndPos;

    // Update is called once per frame
    void Update()
    {
        MoveLaser();
    }

    private void MoveLaser()
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
}
