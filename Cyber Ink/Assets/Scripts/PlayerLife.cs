using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;
    public Animator anim;

    [Header("Player Health")]
    public Image healthBar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    private float lerpSpeed;

    [Header("Health Edits")]
    public float heal;
    public float fallDamage; //out of bounds
    public float slowDamage; //slow trap
    public float trapDamage; //laser and falling object
    public float enemyDamage; //all walking enemies

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
                currentHealth += heal * Time.deltaTime;
            }
        }

        if (currentHealth >= maxHealth) //Set max health
        {
            currentHealth = maxHealth;
        }

        lerpSpeed = 3f * Time.deltaTime;
        SetHealth();
    }
    public void SetHealth()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, lerpSpeed);
    }

    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        Physics2D.IgnoreLayerCollision(3, 8);
        anim.SetLayerWeight(1, 1); //Animator layer 1(gethurt animation), weight 1(visible)
        yield return new WaitForSeconds(1f);
        anim.SetLayerWeight(1, 0); //Animator layer 0(base), weight 0(invisible)
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }
    private void Die()
    {
        playerMovement.knockCounter = 0;
        rb.bodyType = RigidbodyType2D.Static;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(3, 8, true);
        AudioManager.Instance.PlayEffectsOneShot("Death");
        anim.SetTrigger("death");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            currentHealth -= fallDamage;
            if (currentHealth > 0)
            {
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Slow Trap"))
        {
            currentHealth -= slowDamage;
            if (currentHealth > 0)
            {
                playerMovement.checkSlow = true;
                AudioManager.Instance.PlayEffectsOneShot("Slow Trap");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Laser"))
        {
            //Knockback
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockedRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.knockedRight = false;
            }

            currentHealth -= trapDamage;
            if (currentHealth > 0)
            {
                AudioManager.Instance.PlayEffectsOneShot("Laser");
                anim.SetTrigger("getHit");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Laser (H)"))
        {
            //Knockback + More vertical height
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.y <= transform.position.y)
            {
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.knockedTopRight = true;
                }
                if (collision.transform.position.x >= transform.position.x)
                {
                    playerMovement.knockedTopRight = false;
                }
            }
            if (collision.transform.position.y >= transform.position.y)
            {
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.knockedBottomRight = true;
                }
                if (collision.transform.position.x >= transform.position.x)
                {
                    playerMovement.knockedBottomRight = false;
                }
            }

            currentHealth -= trapDamage;
            if (currentHealth > 0)
            {
                AudioManager.Instance.PlayEffectsOneShot("Laser");
                anim.SetTrigger("getHit");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Falling Object"))
        {
            playerMovement.knockCounter = playerMovement.knockTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockedRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.knockedRight = false;
            }

            currentHealth -= trapDamage;
            if (currentHealth > 0)
            {
                AudioManager.Instance.PlayEffectsOneShot("Falling Object");
                anim.SetTrigger("getHit");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= enemyDamage;
            if (currentHealth > 0)
            {
                anim.SetTrigger("getHit");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= enemyDamage;
            if (currentHealth > 0)
            {
                AudioManager.Instance.PlayEffectsOneShot("Bullet");
                anim.SetTrigger("getHit");
                StartCoroutine(GetHurt());
                SetHealth();
            }
            else
            {
                Die();
                SetHealth();
            }
        }
    }
}
