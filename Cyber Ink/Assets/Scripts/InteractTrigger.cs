using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    public bool dialogueTriggered = false;
    public Dialogue interactDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dialogueTriggered == false)
        {
            interactDialogue.StartDialogue();
            dialogueTriggered = true;
        }
    }
}
