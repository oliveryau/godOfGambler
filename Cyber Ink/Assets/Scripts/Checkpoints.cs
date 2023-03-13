using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    private Animator anim;
    private bool passBy = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRespawn = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passBy == false)
        {
            playerRespawn.respawnPoint = transform.position;
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
