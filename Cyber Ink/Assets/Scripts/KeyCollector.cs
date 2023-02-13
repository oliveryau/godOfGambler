using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCollector : MonoBehaviour
{
    [Header("Keys")]
    public GameObject firstKey;
    public GameObject secondKey;
    public GameObject thirdKey;

    [Header("Key UI")]
    public TextMeshProUGUI keyText;
    public int keyCount;

    private void Start()
    {
        keyText.SetText("Keys Collected: " + keyCount + " / 3");
        keyCount = 0;
    }

    private void Update()
    {
        if (firstKey != null)
        {
            FirstKeyCount();
        }

        if (secondKey != null)
        {
            SecondKeyCount();
        }

        if (thirdKey != null)
        {
            ThirdKeyCount();
        }
    }

    private void FirstKeyCount()
    {
        if (Vector2.Distance(transform.position, firstKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            ++keyCount;
            keyText.SetText("Keys Collected: " + keyCount + " / 3");
            Destroy(firstKey);
        }
    }

    private void SecondKeyCount()
    {
        if (Vector2.Distance(transform.position, secondKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            ++keyCount;
            keyText.SetText("Keys Collected: " + keyCount + " / 3");
            Destroy(secondKey);
        }
    }

    private void ThirdKeyCount()
    {
        if (Vector2.Distance(transform.position, thirdKey.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            ++keyCount;
            keyText.SetText("Keys Collected: " + keyCount + " / 3");
            Destroy(thirdKey);
        }
    }
}
