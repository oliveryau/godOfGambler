using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;

    [Header("Settings")]
    public GameObject soundScreen;

    [Header("Credits")]
    public GameObject creditsScreen;

    [Header("Prologue")]
    public DialoguePrologue prologue;

    private void Update()
    {
        if (soundScreen.activeSelf || creditsScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                soundScreen.SetActive(false);
                creditsScreen.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        prologue.StartDialogue();
    }

    public void StartSound()
    {
        soundScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void ExitSound()
    {
        soundScreen.SetActive(false);
    }

    public void StartCredits()
    {
        creditsScreen.SetActive(true);
        soundScreen.SetActive(false);
    }

    public void ExitCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
