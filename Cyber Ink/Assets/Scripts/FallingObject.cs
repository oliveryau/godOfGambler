using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    public bool dropObject;
    public float dropSpeed;
    public float elevateSpeed;

    private void Update()
    {
        SwitchFallingObject();
    }

    private void SwitchFallingObject()
    {
        if (transform.position == startPosition.position)
        {
            dropObject = true;
        }

        if (transform.position == endPosition.position)
        {
            dropObject = false;
        }

        if (dropObject)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.position, dropSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, elevateSpeed * Time.deltaTime);
        }
    }
}
