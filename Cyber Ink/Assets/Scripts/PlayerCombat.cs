using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement playerMovement;
    public LayerMask enemyLayers;

    [Header("Attacking")]
    public bool canAttack;
    public Transform attackPoint;
    public Vector3 attackRange;
    //public int attackDamage = 30;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {

        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public void Attack()
    {
        attackRange = new Vector3(3f, 2f, 0);
        if (playerMovement.rbSprite.flipX == false) //False flipX = Right side attack
        {
            attackPoint.localPosition = new Vector2(2f, 0f);
        }
        else if (playerMovement.rbSprite.flipX == true) //True flipX = Left side attack
        {
            attackPoint.localPosition = new Vector2(-2f, 0f);
        }
        //Play attack animation
        anim.SetTrigger("attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Attacking");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawCube(attackPoint.position, attackRange);
    }
}
