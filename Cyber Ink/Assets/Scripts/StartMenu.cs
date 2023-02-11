using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject credits;
    public LevelFade levelFade;

    public void StartGame()
    {
        StartCoroutine(levelFade.LoadingScene());
        SceneManager.LoadScene("Level 2");
    }

    public void Credits()
    {
        credits.SetActive(true);
        StartCoroutine(CreditsAnimation());
    }

    private IEnumerator CreditsAnimation()
    {
        yield return new WaitForSeconds(20);
        credits.SetActive(false);
    }

    public void ExitGame()
    {
        StartCoroutine(levelFade.LoadingScene());
        Application.Quit();
    }
}
