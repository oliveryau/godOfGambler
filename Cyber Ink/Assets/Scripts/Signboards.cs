using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signboards : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return) && dialogue.activeDialogue == false)
        {
            dialogue.StartDialogue();
        }
    }
}
