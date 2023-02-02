using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D fallCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreLayerCollision(8, 7); //ignores player after hitting player
        }

        if (collision.gameObject.CompareTag("Terrain"))
        {
            Physics2D.IgnoreLayerCollision(8, 7); //ignores player after hitting player
            rb.bodyType = RigidbodyType2D.Static; //makes rb static after hitting terrain
            fallCollider.enabled = false; //makes collider false afterwards
        }
    }
}
