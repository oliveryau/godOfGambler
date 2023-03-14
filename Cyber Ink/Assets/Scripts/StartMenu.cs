using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject titleLogo;
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject creditsButton;
    public GameObject quitButton;


    [Header("Settings")]
    public GameObject soundScreen;

    [Header("Credits")]
    public GameObject creditsScreen;

    [Header("Prologue")]
    public DialoguePrologue prologue;
    private bool playing = false;

    private void Update()
    {
        if (soundScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitSound();
        }
        else if (creditsScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitCredits();
        }
    }

    public void StartGame()
    {
        if (prologue.activeDialogue == false && !playing)
        {
            playing = true;
            prologue.StartDialogue();
        }
    }

    public void StartSound()
    {
        soundScreen.SetActive(true);

        titleLogo.SetActive(false);
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void ExitSound()
    {
        soundScreen.SetActive(false);

        titleLogo.SetActive(true);
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void StartCredits()
    {
        creditsScreen.SetActive(true);

        titleLogo.SetActive(false);
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void ExitCredits()
    {
        creditsScreen.SetActive(false);

        titleLogo.SetActive(true);
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
