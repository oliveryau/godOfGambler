using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeySystem : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public PlayerMovement playerMovement;
    public PauseMenu pauseMenu;

    [Header("Key UI")]
    public TextMeshProUGUI keyText;
    public int keyCount;
    public Dialogue dialogue;

    [Header("Keys")]
    public int maxKeys;
    public GameObject[] keys = new GameObject[] { };

    [Header("Other UI")]
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyPanel;

    private void Start()
    {
        if (sceneManagement.scene.name == "Level 2")
        {
            keyText.SetText("Keycards: " + keyCount + " / 2");
            keyCount = 0;
        }
    }

    private void Update()
    {
        CheckKeys();
    }

    private void CheckKeys()
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            if (keys[i] != null)
            {
                if (Vector2.Distance(transform.position, keys[i].transform.position) < 1f && Input.GetKeyDown(KeyCode.Return) && pauseMenu.isPaused == false)
                {
                    dialogue.StartDialogue();
                    Destroy(keys[i]);
                    ++keyCount;
                    keyText.SetText("Keycards: " + keyCount + " / 2");
                }
            }
        }
    }
}
