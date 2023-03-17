using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    public SceneManagement sceneManagement;

    public GameObject player;
    public Dialogue lastDialogue;
    public PauseMenu pauseMenu;

    [Header("Next Scene")]
    public GameObject dialogue;
    private bool finishGame = false;

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) &&
                pauseMenu.isPaused == false && lastDialogue.activeDialogue == false)
        {
            FinalDialogue();
        }

        FinishingGame();
    }

    private void FinalDialogue()
    {
        lastDialogue.StartDialogue();
        finishGame = true;
    }

    private void FinishingGame()
    {
        if (finishGame && !dialogue.activeSelf)
        {
            StartCoroutine(sceneManagement.FadeNextScene());
        }
    }
}
