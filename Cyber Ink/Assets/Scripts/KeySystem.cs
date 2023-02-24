using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeySystem : MonoBehaviour
{
    public PlayerMovement playerMovement;

    [Header("Keys")]
    public GameObject firstKey;
    public Dialogue firstDialogue;
    public GameObject secondKey;
    public Dialogue secondDialogue;

    [Header("Key UI")]
    public TextMeshProUGUI keyText;
    public int keyCount;

    [Header("Other UI")]
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyPanel;

    private void Start()
    {
        keyText.SetText("Keycards: " + keyCount + " / 2");
        keyCount = 0;
    }

    private void Update()
    {
        if (firstKey != null)
        {
            FirstKey();
        }

        if (secondKey != null)
        {
            SecondKey();
        }
    }

    private void FirstKey()
    {
        if (Vector2.Distance(transform.position, firstKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return))
        {
            firstDialogue.StartDialogue();
            Destroy(firstKey);
            ++keyCount;
            keyText.SetText("Keycards: " + keyCount + " / 2");
        }
    }

    private void SecondKey()
    {
        if (Vector2.Distance(transform.position, secondKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return))
        {
            secondDialogue.StartDialogue();
            Destroy(secondKey);
            ++keyCount;
            keyText.SetText("Keycards: " + keyCount + " / 2");
        }
    }
}
