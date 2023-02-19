using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject credits;

    [Header("UX/UI")]
    public LevelFade levelFade;
    public Animator musicAnim;
    public float waitTime;

    public void StartGame()
    {
        StartCoroutine(MusicFadeChangeScene());
    }
    private IEnumerator MusicFadeChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        StartCoroutine(levelFade.LoadingScene());
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Level 2");
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
