using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public PlayerLife playerLife;

    [Header("Pause")]
    public GameObject pauseScreen;
    public GameObject controlsScreen;
    public GameObject soundScreen;
    public bool isPaused = false;
    public bool noFocus = false;

    [Header("UI")]
    public GameObject healthBar;
    public GameObject keyText;
    public GameObject dashCooldownIcon;

    [Header("Dialogue Pause Settings")]
    public bool isDialogueActive;
    public GameObject[] dialogues = new GameObject[] { };

    [Header("Others")]
    public GameObject gameOverScreen;

    private void Update()
    {
        if (noFocus == true) //Out of the game
        {
            if (!gameOverScreen.activeSelf)
            {
                PauseGame();
            }
        }
        else //Back to the game
        {
            if (Input.GetKeyDown(KeyCode.Escape) && playerLife.currentHealth > 0)
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
            CheckDialogueActive();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
        {
            noFocus = true;
        }
        else
        {
            noFocus = false;
        }
    }

    public void CheckDialogueActive()
    {
        for (int i = 0; i < dialogues.Length; ++i)
        {
            if (dialogues[i].activeSelf)
            {
                isDialogueActive = true;
                break;
            }
            else
            {
                isDialogueActive = false;
            }
        }

        if (isDialogueActive == true)
        {
            if (sceneManagement.scene.name == "Level 1" || sceneManagement.scene.name == "Level 2")
            {
                healthBar.SetActive(false);
            }

            if (sceneManagement.scene.name == "Level 2")
            {
                keyText.SetActive(false);
            }

            dashCooldownIcon.SetActive(false);
        }
        else if (isDialogueActive == false && playerLife.currentHealth > 0 && isPaused == false) //Special setting
        {
            if (sceneManagement.scene.name == "Level 1" || sceneManagement.scene.name == "Level 2")
            {
                healthBar.SetActive(true);
            }

            if (sceneManagement.scene.name == "Level 2")
            {
                keyText.SetActive(true);
            }

            dashCooldownIcon.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        controlsScreen.SetActive(false);
        soundScreen.SetActive(false);
        AudioManager.Instance.effectsSource.Pause();

        isPaused = true;

        if (sceneManagement.scene.name == "Level 1" || sceneManagement.scene.name == "Level 2")
        {
            healthBar.SetActive(false);
        }

        if (sceneManagement.scene.name == "Level 2")
        {
            keyText.SetActive(false);
        }

        dashCooldownIcon.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        AudioManager.Instance.effectsSource.UnPause();

        isPaused = false;

        if (sceneManagement.scene.name == "Level 1" || sceneManagement.scene.name == "Level 2")
        {
            healthBar.SetActive(true);
        }

        if (sceneManagement.scene.name == "Level 2")
        {
            keyText.SetActive(true);
        }

        dashCooldownIcon.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RetryGame()
    {
        isPaused = false;

        StartCoroutine(sceneManagement.FadeSameScene());
        Time.timeScale = 1f;
    }

    public void GoToControls()
    {
        controlsScreen.SetActive(true);
        soundScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void GoToSound()
    {
        controlsScreen.SetActive(false);
        soundScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void BackToPause()
    {
        controlsScreen.SetActive(false);
        soundScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void GoToMenu()
    {
        isPaused = false;

        StartCoroutine(sceneManagement.FadeMenu());
        Time.timeScale = 1f;
    }
}
