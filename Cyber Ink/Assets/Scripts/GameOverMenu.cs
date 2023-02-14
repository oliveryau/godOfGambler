using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerLife playerLife;
    public LevelFade levelFade;
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyText;
    public GameObject gameOverScreen;

    private PlayerRespawn playerRespawn;

    private void Start()
    {
        playerRespawn = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if (playerLife.currentHealth < 0)
        {
            StartCoroutine(DelayMenu());
        }
    }

    public void RetryGame()
    {
        StopAllCoroutines();
        playerRespawn.RespawnNow();
        playerLife.currentHealth = 100f;
        playerLife.rb.bodyType = RigidbodyType2D.Dynamic;
        playerMovement.canMove = true;
        playerMovement.canJump = true;
        healthBar.enabled = true;
        dashCooldownImage.enabled = true;
        keyText.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        StartCoroutine(levelFade.LoadingScene());
        SceneManager.LoadScene("Start");
        gameOverScreen.SetActive(false);
    }

    public IEnumerator DelayMenu()
    {
        playerMovement.canMove = false;
        playerMovement.canJump = false;
        yield return new WaitForSeconds(2);
        gameOverScreen.SetActive(true);
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyText.SetActive(false);
    }
}
