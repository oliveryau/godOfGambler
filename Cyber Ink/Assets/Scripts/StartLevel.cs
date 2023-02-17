using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public Dialogue startDialogue;

    // Start is called before the first frame update
    void Start()
    {
        startDialogue.StartDialogue();
    }
}
