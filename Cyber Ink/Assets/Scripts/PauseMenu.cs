using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public PlayerLife playerLife;

    [Header("Pause")]
    public GameObject pauseScreen;
    public GameObject controlsScreen;
    public bool isPaused = false;

    [Header("UI")]
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyText;

    [Header("Dialogue Pause Settings")]
    public GameObject missingKeyPanel;
    public GameObject startDialogue;
    public GameObject firstDialogue;
    public GameObject secondDialogue;
    public GameObject thirdDialogue;
    public bool isPanelActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerLife.currentHealth > 0 && isPanelActive == false)
        {
            if (isPaused && pauseScreen.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        CheckActiveDialogue();
    }

    public void CheckActiveDialogue()
    {
        if (missingKeyPanel.activeSelf || startDialogue.activeSelf || firstDialogue.activeSelf || secondDialogue.activeSelf || thirdDialogue.activeSelf)
        {
            isPanelActive = true;
        }
        else
        {
            isPanelActive = false;
        }

        if (isPanelActive == true)
        {
            healthBar.enabled = false;
            dashCooldownImage.enabled = false;
            keyText.SetActive(false);
        }
        else if (isPanelActive == false && playerLife.currentHealth > 0 && isPaused == false) //Special setting
        {
            healthBar.enabled = true;
            dashCooldownImage.enabled = true;
            keyText.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        controlsScreen.SetActive(false);
        isPaused = true;

        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyText.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        isPaused = false;

        healthBar.enabled = true;
        dashCooldownImage.enabled = true;
        keyText.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GoToControls()
    {
        pauseScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void BackToPause()
    {
        controlsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void GoToMenu()
    {
        isPaused = false;

        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }
}
