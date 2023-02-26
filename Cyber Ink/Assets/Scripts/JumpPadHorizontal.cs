using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadHorizontal : MonoBehaviour
{
    public Vector2 bounceAngle;
    public float bounceForce;

    public float bounceTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ExternalForce(collision));
        }
    }

    private IEnumerator ExternalForce(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerMovement>().SetExternalForce(true);
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceAngle * bounceForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(bounceTime);
        collision.gameObject.GetComponent<PlayerMovement>().SetExternalForce(false);
    }
}
