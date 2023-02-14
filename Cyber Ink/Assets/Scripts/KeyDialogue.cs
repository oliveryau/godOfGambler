using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyDialogue : MonoBehaviour
{
    public KeyCollector keyCollector;

    public GameObject dialoguePanel;
    public TextMeshProUGUI mainText;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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

    public void StartDialogue()
    {
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
            gameObject.SetActive(false);
            keyCollector.KeyCount();
        }
    }
}
