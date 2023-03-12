using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeySystem : MonoBehaviour
{
    public SceneManagement sceneManagement;
    public PauseMenu pauseMenu;

    [Header("Key UI")]
    public TextMeshProUGUI keyText;
    public int keyCount;
    public Dialogue dialogue;

    [Header("Keys")]
    public int maxKeys;
    public GameObject[] keys = new GameObject[] { };

    private void Start()
    {
        keyText.SetText("Keycards: " + keyCount + " / " + maxKeys);
        keyCount = 0;
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
                if (Vector2.Distance(transform.position, keys[i].transform.position) < 1.5f && Input.GetKeyDown(KeyCode.E) && pauseMenu.isPaused == false)
                {
                    dialogue.StartDialogue();
                    Destroy(keys[i]);
                    ++keyCount;
                    keyText.SetText("Keycards: " + keyCount + " / " + maxKeys);
                }
            }
        }
    }
}
