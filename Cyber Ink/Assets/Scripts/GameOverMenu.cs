using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public PlayerLife playerLife;
    public PlayerMovement playerMovement;

    [Header("UI")]
    public GameObject healthBar;
    public GameObject keyText;
    public Image dashCooldownImage;
    public GameObject gameOverScreen;

    private PlayerRespawn playerRespawn;

    private void Start()
    {
        playerRespawn = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if (playerLife.currentHealth <= 0)
        {
            StartCoroutine(DelayMenu());
        }
    }

    public IEnumerator DelayMenu()
    {
        yield return new WaitForSeconds(2f);
        gameOverScreen.SetActive(true);
        healthBar.SetActive(false);
        dashCooldownImage.enabled = false;
        if (sceneManagement.scene.name == "Level 2")
        {
            keyText.SetActive(false);
        }
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        StopAllCoroutines();
        playerLife.currentHealth = 100f;
        playerLife.rb.bodyType = RigidbodyType2D.Dynamic;
        playerMovement.anim.SetInteger("state", 0); //Idle anim
        playerRespawn.RespawnNow();
        healthBar.SetActive(true);
        dashCooldownImage.enabled = true;
        if (sceneManagement.scene.name == "Level 2")
        {
            keyText.SetActive(true);
        }
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        StopAllCoroutines();
        StartCoroutine(sceneManagement.FadeMenu());
        Time.timeScale = 1f;
    }
}
