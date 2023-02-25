using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerLife playerLife;
    public Vector2 respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            RespawnNow();
        }
    }
    public void RespawnNow()
    {
        if (playerLife.currentHealth > 0)
        {
            transform.position = respawnPoint;
        }
        else
        {
            return;
        }
    }
}
