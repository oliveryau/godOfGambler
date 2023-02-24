using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject player;
    public Dialogue dialogue;
    public SceneManagement sceneManagement;

    [Header("Keys")]
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
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return) && dialogue.activeDialogue == false)
        {
            if (keySystem.keyCount != 2)
            {
                MissingKey();
            }
            else
            {
                StartCoroutine(sceneManagement.MusicFadeChangeScene());
            }
        }
    }

    private void MissingKey()
    {
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyPanel.SetActive(false);
        dialogue.StartDialogue();
    }
}
