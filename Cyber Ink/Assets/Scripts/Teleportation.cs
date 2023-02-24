using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject portal;
    public GameObject respectiveKey;
    public PlayerRespawn playerRespawn;

    [Header("Dialogue")]
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
        CheckTeleport();
    }

    public void CheckTeleport()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.Return) && dialogue.activeDialogue == false)
        {
            if (respectiveKey != null)
            {
                dialogue.StartDialogue();
            }
            else
            {
                player.transform.position = portal.transform.position;
                playerRespawn.respawnPoint = new Vector2(100f, -27f);
            }
        }
    }
}
