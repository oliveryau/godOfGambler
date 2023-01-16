using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    //private Rigidbody2D rb;
    private Animator anim; //Trigger animation
    public GameObject respawnPoint;

    [Header("Player Health")]
    public int currentHealth;
    public int maxHealth = 100;
    public Slider slider;

    [Header("Objects")]
    public int heals = 10;
    public int normalDamage = 20;

    //public float timeRemaining = 120f;
    //public bool timeRun = false;
    //public Text timeText;
    //public Text redTimeText; //final few seconds warning

    public GameObject gameOverScreen; //Game over screen

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Respawn");
    }

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //Get animation component
        //timeRun = true;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        Damage(10); //Can test if health / attacking is working
    //    }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Heal(10);
        //}

        //if (timeRemaining > 0)
        //{
        //    timeRemaining -= Time.deltaTime;
        //    timeText.text = "Time Left: " + (int)timeRemaining;
        //    if (timeRemaining < 31) //Little time left
        //    {
        //        timeText.text = "";
        //        redTimeText.text = "Time Left: " + (int)timeRemaining;
        //    }
        //}
        //else
        //{
        //    rb.bodyType = RigidbodyType2D.Static; //Make player rb unable to move
        //    anim.SetTrigger("death"); //Animation state/condition
        //    timeRemaining = 0f;
        //    timeRun = false;
        //    gameOverScreen.SetActive(true);
        //}
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (currentHealth == 0)
            {
                Die();
                SetHealth(currentHealth);
            }
            else
            {
                currentHealth -= normalDamage;
                SetHealth(currentHealth);
                StartCoroutine(GetHurt());
            }
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
            if (currentHealth == 0)
            {
                Die();
                SetHealth(currentHealth);
            }
            else
            {
                currentHealth -= normalDamage;
                SetHealth(currentHealth);
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt() //can use for hearts too
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        anim.SetLayerWeight(1, 1); //Animator layer 1(gethurt animation), weight 1(visible)
        yield return new WaitForSeconds(3);
        anim.SetLayerWeight(1, 0); //Animator layer 0(base), weight 0(invisible)
        Physics2D.IgnoreLayerCollision(7, 8, false);
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

    //public void Damage (int amount)
    //{
    //    if (amount < 0)
    //    {
    //        throw new System.ArgumentOutOfRangeException("No negative damage");
    //    }
    //    currentHealth -= amount;
    //    SetHealth(currentHealth);

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //public void Heal(int amount)
    //{
    //    if (amount < 0)
    //    {
    //        throw new System.ArgumentOutOfRangeException("No negative healing");
    //    }

    //    if (currentHealth + amount > maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //    }
    //    else
    //    {
    //        currentHealth += amount;
    //    }
    //}

    private void Die()
    {
        //rb.bodyType = RigidbodyType2D.Static; //Make player rb unable to move
        anim.SetTrigger("death"); //Animation state/condition
        gameOverScreen.SetActive(true);
    }
}
