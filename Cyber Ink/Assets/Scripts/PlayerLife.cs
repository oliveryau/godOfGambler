using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim; //Trigger animation
    private PlayerMovement playerMovement;
    public GameObject respawnPoint;
    public GameObject gameOverScreen; //Game over screen

    [Header("Player Health")]
    public Slider slider;
    public int currentHealth = 100;
    public int maxHealth = 100;

    [Header("Health Edits")]
    public int heals = 20;
    public int fallDamage = 20;
    public int slowDamage = 10;
    public int trapDamage = 20;
    public int enemyDamage = 30;


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
        SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //Test if attack/healing is working
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Damage(10);
        //}

        if (playerMovement.checkSlow) //Slow debuff timer
        {
            playerMovement.moveSpeed = 3f;
            playerMovement.slowTimer += Time.deltaTime;
            if (playerMovement.slowTimer >= 3) //3 second debuff
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
            SetHealth(currentHealth);
            StartCoroutine(GetHurt());
        }
        else
        {
            //player die
            Die();
            SetHealth(currentHealth);
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
        if (collision.gameObject.CompareTag("Heal"))
        {
            if (currentHealth < maxHealth)
            {
                Destroy(collision.gameObject);
                currentHealth += heals;
                SetHealth(currentHealth);
            }
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            FallDamage();
            Debug.Log("Hi");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage();
        }
    }

    IEnumerator GetHurt() //can use for hearts too
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        Physics2D.IgnoreLayerCollision(3, 8);
        anim.SetLayerWeight(1, 1); //Animator layer 1(gethurt animation), weight 1(visible)
        yield return new WaitForSeconds(3);
        anim.SetLayerWeight(1, 0); //Animator layer 0(base), weight 0(invisible)
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; //Max value of healthbar = currentHealth which is 100
        slider.value = health; //Making sure the slider starts at maxHealth
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void FallDamage()
    {
        currentHealth -= fallDamage;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth(currentHealth);
        }
        else
        {
            SetHealth(currentHealth);
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
            SetHealth(currentHealth);
        }
        else
        {
            SetHealth(currentHealth);
            StartCoroutine(GetHurt());
        }
    }

    public void TrapDamage()
    {
        currentHealth -= trapDamage;
        if (currentHealth <= 0)
        {
            Die();
            SetHealth(currentHealth);
        }
        else
        {
            SetHealth(currentHealth);
            StartCoroutine(GetHurt());
        }
    }

    public void EnemyDamage()
    {
        currentHealth -= enemyDamage; // should we have different damage for enemy?
        if (currentHealth <= 0)
        {
            Die();
            SetHealth(currentHealth);
        }
        else
        {
            SetHealth(currentHealth);
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
