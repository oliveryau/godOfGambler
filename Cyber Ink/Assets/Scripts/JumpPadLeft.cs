using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadLeft : MonoBehaviour
{
    public float bounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bounce, ForceMode2D.Impulse);
        }
    }
}
