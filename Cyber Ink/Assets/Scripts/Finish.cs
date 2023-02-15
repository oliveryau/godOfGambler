using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [Header("UX/UI")]
    public LevelFade levelFade;
    public GameObject missingKeyScreen;

    [Header("Keys")]
    public GameObject firstKey;
    public GameObject secondKey;
    public GameObject thirdKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CheckLevelComplete();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CheckLevelCompleteExit();
        }
    }

    public void CheckLevelComplete()
    {
        if (firstKey != null || secondKey != null || thirdKey != null)
        {
            missingKeyScreen.SetActive(true);
        }
        else
        {
            StartCoroutine(levelFade.LoadingScene());
            SceneManager.LoadScene("Start");
        }
    }

    public void CheckLevelCompleteExit()
    {
        missingKeyScreen.SetActive(false);
    }
}
