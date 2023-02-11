using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCollector : MonoBehaviour
{
    public GameObject key;
    public TextMeshProUGUI keyText;
    public int keyCount;

    private bool getKey;

    private void Start()
    {
        keyText.SetText("Key: " + keyCount + " / 1");
        keyCount = 0;
        getKey = false;
    }

    private void Update()
    {
        if (key != null)
        {
            IncrementKeyCount();
        }
    }

    private void IncrementKeyCount()
    {
        if (Vector2.Distance(transform.position, key.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            getKey = true;
        }

        if (getKey)
        {
            ++keyCount;
            keyText.SetText("Key: " + keyCount + " / 1");
            Destroy(key);
        }
    }
}
