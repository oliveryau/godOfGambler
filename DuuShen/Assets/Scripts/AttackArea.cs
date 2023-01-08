using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 5;
    public SpriteRenderer shortAttack;

    private void Start()
    {
        shortAttack = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 attackScale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            attackScale.x = -2;
            transform.eulerAngles = new Vector3(180, 0);
            //shortAttack.flipX = true;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            attackScale.x = 2;
            transform.eulerAngles = new Vector3(0, 0);
            //shortAttack.flipX = false;
        }
        transform.localScale = attackScale;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var checkHealth = collision.GetComponent<PlayerLife>();

        if (checkHealth != null)
        {
            PlayerLife health = checkHealth;
            health.Damage(damage);
        }
    }
}
