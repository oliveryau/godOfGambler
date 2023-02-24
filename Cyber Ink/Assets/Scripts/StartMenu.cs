using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject credits;
    public SceneManagement sceneManagement;

    public void StartGame()
    {
        StartCoroutine(sceneManagement.MusicFadeChangeScene());
    }

    public void Credits()
    {
        StartCoroutine(CreditsAnimation());
    }

    private IEnumerator CreditsAnimation()
    {
        credits.SetActive(true);
        yield return new WaitForSeconds(15);
        credits.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
