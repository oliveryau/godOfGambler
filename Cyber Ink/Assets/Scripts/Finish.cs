using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Others")]
    public GameObject player;

    private void Update()
    {
        CheckLevelComplete();

        if (missingKeyScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                missingKeyScreen.SetActive(false);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        CheckLevelComplete();
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        CheckLevelCompleteExit();
    //    }
    //}

    public void CheckLevelComplete()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
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
        

    }

    //public void CheckLevelCompleteExit()
    //{
    //    missingKeyScreen.SetActive(false);
    //}
}
