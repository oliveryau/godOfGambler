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

    public void StartGame()
    {
        StartCoroutine(sceneManagement.FadeNextScene());
    }

    public void StartSound()
    {
        soundScreen.SetActive(true);
    }

    public void ExitSound()
    {
        soundScreen.SetActive(false);
    }

    public void StartCredits()
    {
        creditsScreen.SetActive(true);
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
