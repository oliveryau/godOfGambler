using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject missingKeyScreen;
    public GameObject key;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

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
        if (key != null)
        {
            missingKeyScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Zephan's New Level");
        }
    }

    public void CheckLevelCompleteExit()
    {
        missingKeyScreen.SetActive(false);
    }
}
