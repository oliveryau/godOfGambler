using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 5;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerLife>() != null)
        {
            PlayerLife health = collision.GetComponent<PlayerLife>();
            health.Damage(damage);
        }
    }
}
