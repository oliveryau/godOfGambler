using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCollector : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyPanel;

    [Header("Keys")]
    public GameObject firstKey;
    public KeyDialogue firstDialogue;
    public GameObject secondKey;
    public KeyDialogue secondDialogue;
    public GameObject thirdKey;
    public KeyDialogue thirdDialogue;

    [Header("Key UI")]
    public TextMeshProUGUI keyText;
    public int keyCount;

    private void Start()
    {
        keyText.SetText("Keycards: " + keyCount + " / 3");
        keyCount = 0;
    }

    private void Update()
    {
        if (firstKey != null)
        {
            FirstKeyDialogue();
        }

        if (secondKey != null)
        {
            SecondKeyDialogue();
        }

        if (thirdKey != null)
        {
            ThirdKeyDialogue();
        }
    }
    public void KeyCount()
    {
        ++keyCount;
        keyText.SetText("Keycards: " + keyCount + " / 3");
        healthBar.enabled = true;
        dashCooldownImage.enabled = true;
        keyPanel.SetActive(true);
        playerMovement.canMove = true;
        playerMovement.canJump = true;
    }

    private void FirstKeyDialogue()
    {
        if (Vector2.Distance(transform.position, firstKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            healthBar.enabled = false;
            dashCooldownImage.enabled = false;
            keyPanel.SetActive(false);
            playerMovement.canMove = false;
            playerMovement.canJump = false;
            firstDialogue.StartDialogue();
            Destroy(firstKey);
        }
    }


    private void SecondKeyDialogue()
    {
        if (Vector2.Distance(transform.position, secondKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            healthBar.enabled = false;
            dashCooldownImage.enabled = false;
            keyPanel.SetActive(false);
            playerMovement.canMove = false;
            playerMovement.canJump = false;
            secondDialogue.StartDialogue();
            Destroy(secondKey);
        }
    }

    public void ThirdKeyDialogue()
    {
        if (Vector2.Distance(transform.position, thirdKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            healthBar.enabled = false;
            dashCooldownImage.enabled = false;
            keyPanel.SetActive(false);
            playerMovement.canMove = false;
            playerMovement.canJump = false;
            thirdDialogue.StartDialogue();
            Destroy(thirdKey);
        }
    }
}
