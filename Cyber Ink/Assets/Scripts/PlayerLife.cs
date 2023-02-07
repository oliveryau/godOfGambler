using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject respawnPoint;
    public GameObject gameOverScreen; //Game over screen

    [Header("Player Health")]
    public Image healthBar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    private float lerpSpeed;

    [Header("Health Edits")]
    public float heal = 1f;
    public float fallDamage = 10f;
    public float slowDamage = 10f;
    public float trapDamage = 10f;
    public float enemyDamage = 20f;


    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Respawn");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth < maxHealth) //Health regen
        {
            currentHealth += heal * Time.deltaTime;
        }

        if (currentHealth >= maxHealth) //Set max health
        {
            currentHealth = maxHealth;
        }

        lerpSpeed = 3f * Time.deltaTime;
        SetHealth();
        HealthBarColor();

        if (playerMovement.checkSlow) //Slow debuff timer
        {
            playerMovement.moveSpeed = 3f;
            playerMovement.slowTimer += Time.deltaTime;
            if (playerMovement.slowTimer >= 1.5f) //3 second debuff
            {
                playerMovement.moveSpeed = 11f;
                playerMovement.slowTimer = 0f;
                playerMovement.checkSlow = false;
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - (int)_damage, 0, maxHealth);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TrapDamage();
        }

        if (collision.gameObject.CompareTag("Slow Trap"))
        {
            SlowDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            FallDamage();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage();
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

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static; //Make player rb unable to move
        anim.SetTrigger("death"); //Animation state/condition
        gameOverScreen.SetActive(true);
    }
}
