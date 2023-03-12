using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public GameObject player;
    public Dialogue successDialogue;
    public Dialogue failDialogue;
    public PauseMenu pauseMenu;

    [Header("Finish Condition")]
    public KeySystem keySystem;

    private Animator anim;
    private bool passBy = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (sceneManagement.scene.name == "Level 1")
        {
            LevelOneCheck();
        }
        else if (sceneManagement.scene.name == "Level 2")
        {
            LevelTwoKeyCheck();
        }
    }

    public void LevelOneCheck()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) &&
            pauseMenu.isPaused == false && successDialogue.activeDialogue == false)
        {
            StartCoroutine(FinishingLevel());
        }
    }

    public void LevelTwoKeyCheck()
    {
        if (keySystem.keyCount != keySystem.maxKeys) //Unsuccessful
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) && 
                pauseMenu.isPaused == false && failDialogue.activeDialogue == false)
            {
                MissingKey();
            }
        }
        else //Successful
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) &&
                pauseMenu.isPaused == false && successDialogue.activeDialogue == false)
            {
                StartCoroutine(FinishingLevel());
            }
        }
    }

    private void MissingKey()
    {
        failDialogue.StartDialogue();
    }

    private IEnumerator FinishingLevel()
    {
        successDialogue.StartDialogue();
        yield return new WaitForSeconds(1f);
        StartCoroutine(sceneManagement.FadeNextScene());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passBy == false)
        {
            passBy = true;
            anim.SetTrigger("glow");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passBy == true)
        {
            passBy = false;
            anim.SetTrigger("fadeOut");
        }
    }
}
