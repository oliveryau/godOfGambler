using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private PlayerMovement playerMovement;
    //private PlayerAttack playerAttack;
    //private bool canAttack;
    //private bool isAttacking;
    //private bool isAttackingCooldown;
    //private float attackingCooldown = 0.5f;
    //private Vector2 startPosition;

    private void Awake()
    {
        //startPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector2 attackScale = transform.localScale;
        transform.localScale = attackScale;

        //if (isAttacking)
        //{
        //    return;
        //}

        if (playerMovement.IsGrounded())
        {
            transform.localPosition = new Vector2(0, -1.5f);
        }

        //if (canAttack)
        //{
        //    StartCoroutine(Attack());
        //}

        //if (!isAttackingCooldown)
        //{
        //    canAttack = true;
        //    if (playerMovement.IsGrounded())
        //    {
        //        canAttack = false;
        //    }
        //}
    }

    private void FixedUpdate()
    {
        //if (isAttacking)
        //{
        //    return;
        //}
    }

    //private IEnumerator Attack()
    //{
    //    canAttack = false;
    //    isAttacking = true;
    //    isAttackingCooldown = true;
    //    yield return new WaitForSeconds(playerAttack.timeToAttack);
    //    isAttacking = false;
    //    yield return new WaitForSeconds(attackingCooldown);
    //    canAttack = true;
    //    isAttackingCooldown = false;

    //}

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Collectible"))
    //    {
    //        Debug.Log("Hi");
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //    }

    //    //if (checkObject != null)
    //    //{
    //    //    PlayerLife health = checkObject;
    //    //    health.Damage(damage);
    //    //}
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Attack"))
    //    {
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //    }
    //}
}
