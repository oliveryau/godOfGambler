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
    public TextMeshProUGUI keyText;
    public Image healthBar;
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
            gameOverScreen.SetActive(true);
            healthBar.enabled = false;
            keyText.enabled = false;
        }
    }

    public void RetryGame()
    {
        playerRespawn.RespawnNow();
        playerLife.currentHealth = 100f;
        playerLife.rb.bodyType = RigidbodyType2D.Dynamic;
        healthBar.enabled = true;
        keyText.enabled = true;
        gameOverScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start");
        gameOverScreen.SetActive(false);
    }
}
