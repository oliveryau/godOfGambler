using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;
    private Animator anim;

    [Header("Player Health")]
    public Image healthBar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    private float lerpSpeed;

    [Header("Health Edits")]
    public float heal = 5f;
    public float fallDamage = 30f; //out of bounds
    public float slowDamage = 30f; //slow trap
    public float trapDamage = 50f; //laser and falling object
    public float enemyDamage = 50f; //all walking enemies

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Respawn");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth > 0)
        {
            if (currentHealth < maxHealth) //Health regen
            {
                if (rb.velocity == Vector2.zero)
                {
                    currentHealth += heal * Time.deltaTime;
                }
            }
        }

        if (currentHealth >= maxHealth) //Set max health
        {
            currentHealth = maxHealth;
        }

        lerpSpeed = 3f * Time.deltaTime;
        SetHealth();
        HealthBarColor();

        if (playerMovement.checkSlow == true) //Slow debuff timer
        {
            playerMovement.moveSpeed = 6f;
            playerMovement.slowTimer += Time.deltaTime;
            if (playerMovement.slowTimer >= 1.5f) //1.5 second debuff
            {
                playerMovement.moveSpeed = 10f;
                playerMovement.slowTimer = 0f;
                playerMovement.checkSlow = false;
            }
        }
    }

    private IEnumerator GetHurt() //can use for hearts too
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        Physics2D.IgnoreLayerCollision(3, 8);
        anim.SetLayerWeight(1, 1); //Animator layer 1(gethurt animation), weight 1(visible)
        yield return new WaitForSeconds(1.5f);
        anim.SetLayerWeight(1, 0); //Animator layer 0(base), weight 0(invisible)
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }

    public void SetHealth()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, lerpSpeed);
    }

    private void HealthBarColor()
    {
        Color healthColor = Color.Lerp(Color.red, Color.cyan, (currentHealth / maxHealth));
        healthBar.color = healthColor;
    }

    public void FallDamage()
    {
        currentHealth -= fallDamage;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth();
        }
        else
        {
            SetHealth();
            StartCoroutine(GetHurt());
        }
    }

    public void SlowDamage()
    {
        currentHealth -= slowDamage;
        playerMovement.checkSlow = true;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth();
        }
        else
        {
            SetHealth();
            StartCoroutine(GetHurt());
        }
    }

    public void TrapDamage()
    {
        currentHealth -= trapDamage;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth();
        }
        else
        {
            SetHealth();
            StartCoroutine(GetHurt());
        }
    }

    public void EnemyDamage()
    {
        currentHealth -= enemyDamage;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth();
        }
        else
        {
            SetHealth();
            StartCoroutine(GetHurt());
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            //player hurt
            SetHealth();
            StartCoroutine(GetHurt());
        }
        else
        {
            //player die
            Die();
            SetHealth();
        }
    }

    private void Die()
    {
        playerMovement.knockCounter = 0;
        rb.bodyType = RigidbodyType2D.Static; //Make player rb unable to move
        anim.SetTrigger("death"); //Animation state/condition
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            FallDamage();
        }

        if (collision.gameObject.CompareTag("Slow Trap"))
        {
            SlowDamage();
        }

        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Falling Object"))
        {
            //Knockback
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.knockRight = false;
            }

            TrapDamage();
        }

        if (collision.gameObject.CompareTag("Laser (H)"))
        {
            //Knockback + more vertical
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.y <= transform.position.y)
            {
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.knockTop = true;
                    playerMovement.knockRight = true;
                }
                if (collision.transform.position.x >= transform.position.x)
                {
                    playerMovement.knockTop = true;
                    playerMovement.knockRight = false;
                }
            }
            if (collision.transform.position.y >= transform.position.y)
            {
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.knockTop = false;
                    playerMovement.knockRight = true;
                }
                if (collision.transform.position.x >= transform.position.x)
                {
                    playerMovement.knockTop = false;
                    playerMovement.knockRight = false;
                }
            }

            TrapDamage();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Knockback
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.knockRight = false;
            }

            EnemyDamage();
        }
    }
}
