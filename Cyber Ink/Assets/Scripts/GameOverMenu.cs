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

    [Header("UI")]
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
        if (playerLife.currentHealth <= 0)
        {
            StartCoroutine(DelayMenu());
        }
    }
    public IEnumerator DelayMenu()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyText.SetActive(false);
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        StopAllCoroutines();
        playerLife.currentHealth = 100f;
        playerLife.rb.bodyType = RigidbodyType2D.Dynamic;
        playerRespawn.RespawnNow();
        healthBar.enabled = true;
        dashCooldownImage.enabled = true;
        keyText.SetActive(true);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        StopAllCoroutines();
        StartCoroutine(sceneManagement.MusicFadeChangeScene());
        Time.timeScale = 1f;
    }
}
