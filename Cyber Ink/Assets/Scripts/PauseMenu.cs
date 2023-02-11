using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image healthBar;
    public Image dashCooldownImage;
    public TextMeshProUGUI keyText;

    public GameObject pauseScreen;
    public bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyText.enabled = false;
        Time.timeScale = 0f;
        playerMovement.canMove = false;
        playerMovement.canJump = false;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        healthBar.enabled = true;
        dashCooldownImage.enabled = true;
        keyText.enabled = true;
        Time.timeScale = 1f;
        playerMovement.canMove = true;
        playerMovement.canJump = true;
        isPaused = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
        playerMovement.canMove = true;
        playerMovement.canJump = true;
        isPaused = false;
    }
}
