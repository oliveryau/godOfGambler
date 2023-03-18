using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
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
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            mainText.text += c;
            AudioManager.Instance.PlayEffectsOneShot("Bullet");
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            mainText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            activeDialogue = false;
            gameObject.SetActive(false);
        }
    }
}
