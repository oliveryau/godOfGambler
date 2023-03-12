using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public SceneManagement sceneManagement;

    [Header("Triggers")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI mainText;
    public PauseMenu pauseMenu;

    [Header("Dialogue")]
    public string[] lines;
    public float textSpeed;
    public bool activeDialogue;

    private int index;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (pauseMenu.isPaused == false)
            {
                if (mainText.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    mainText.text = lines[index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        activeDialogue = true;
        dialoguePanel.SetActive(true);
        mainText.text = string.Empty;
        index = 0;
        AudioManager.Instance.PlayEffects("Dialogue");
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            mainText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            mainText.text = string.Empty;
            AudioManager.Instance.PlayEffects("Dialogue");
            StartCoroutine(TypeLine());
        }
        else
        {
            activeDialogue = false;
            AudioManager.Instance.effectsSource.Stop();
            if (sceneManagement.scene.name == "Level 3")
            {
                StartCoroutine(sceneManagement.FadeNextScene());
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }
}
