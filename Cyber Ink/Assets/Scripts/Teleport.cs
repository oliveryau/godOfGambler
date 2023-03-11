using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject portal;
    public GameObject teleportCondition;
    public PlayerRespawn playerRespawn;
    public PauseMenu pauseMenu;

    [Header("Dialogue")]
    public Dialogue dialogue;
    private GameObject player;

    [Header("Animations")]
    public Animator destinationAnim;
    private Animator anim;
    private bool passBy = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTeleport();
    }

    public void CheckTeleport()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3f && Input.GetKeyDown(KeyCode.E) && dialogue.activeDialogue == false && pauseMenu.isPaused == false)
        {
            if (teleportCondition != null)
            {
                dialogue.StartDialogue();
            }
            else
            {
                player.transform.position = portal.transform.position;
                playerRespawn.respawnPoint = new Vector2(10f, 0f);
                destinationAnim.SetTrigger("glow");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passBy == false)
        {
            passBy = true;
            anim.SetTrigger("glow");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passBy == true)
        {
            passBy = false;
            anim.SetTrigger("fadeOut");
        }
    }
}
