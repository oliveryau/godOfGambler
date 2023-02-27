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

    [Header("Others")]
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyPanel;

    private void Update()
    {
        if (sceneManagement.scene.name == "Level 2")
        {
            LevelTwoKeyCheck();
        }
    }

    public void LevelTwoKeyCheck()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return) && pauseMenu.isPaused == false)
        {
            if (failDialogue.activeDialogue == false || successDialogue.activeDialogue == false)
            {
                if (keySystem.keyCount != keySystem.maxKeys)
                {
                    MissingKey();
                }
                else
                {
                    StartCoroutine(FinishingLevel());
                }
            }
        }
    }

    private void MissingKey()
    {
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyPanel.SetActive(false);
        failDialogue.StartDialogue();
    }

    private IEnumerator FinishingLevel()
    {
        successDialogue.StartDialogue();
        yield return new WaitForSeconds(1f);
        StartCoroutine(sceneManagement.MusicFadeChangeScene());
    }
}
