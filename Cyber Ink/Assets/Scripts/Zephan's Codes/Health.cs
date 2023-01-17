using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private int health = 100;

    [SerializeField]
    private Text statusText;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }

    }

    public void Heal (int amount)
    {
        if(amount < 0 )
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldbeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldbeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }

        else
        {
            this.health += amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy")) //Check Enemy tag
        {
            Destroy(collider.gameObject);
            statusText.text = "Enemy Defeated!";

        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static; //Make player rb unable to move
        anim.SetTrigger("death");
    }
}
