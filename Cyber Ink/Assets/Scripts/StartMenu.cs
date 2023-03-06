using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;

    [Header("Credits")]
    public GameObject creditsScreen;

    public void StartGame()
    {
        StartCoroutine(sceneManagement.MusicFadeNextScene());
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
