using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int counter = 0;
    private int collectible = 20;
    private int slows = 20;
    private float statusTimer;

    [SerializeField] public Text counterText;
    [SerializeField] private Text statusText;
    [SerializeField] private Text collectibleCount;
    [SerializeField] private Text slowsCount;

    private void Start()
    {
        statusTimer = 0f;
    }

    private void Update()
    {
        statusTimer += Time.deltaTime;
        if (statusTimer >= 2)
        {
            statusTimer = 0f;
            statusText.text = ""; //To make status message disappear with debuff
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Put IsTrigger on Unity
    {
        if (collision.gameObject.CompareTag("Collectible")) //Check it is Collectible tag
        {
            Destroy(collision.gameObject);
            statusText.text = "Speed Boost!";
            counter++;
            collectible--;
        }

        if (collision.gameObject.CompareTag("Slows")) //Check Slows tag
        {
            Destroy(collision.gameObject);
            statusText.text = "Slowed! Score decreased!\nAvoid these foods!";
            if (counter > 0) //Loop is for minusing score
            {
                --counter;
            }
            else
            {
                counter = 0;
            }
            slows--;
        }

        counterText.text = "Score: " + counter;
        collectibleCount.text = "x" + collectible;
        slowsCount.text = "x" + slows;
    }
}
