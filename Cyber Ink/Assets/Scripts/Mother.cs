using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    public GameObject player;
    public Dialogue dialogue;
    public PauseMenu pauseMenu;

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) &&
                pauseMenu.isPaused == false && dialogue.activeDialogue == false)
        {
            FinishingGame();
        }
    }

    private void FinishingGame()
    {
        dialogue.StartDialogue();
    }
}
