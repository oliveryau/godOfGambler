using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerLife playerLife;
    public PlayerMovement playerMovement;
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
            Physics2D.IgnoreLayerCollision(7, 8, false);
            Physics2D.IgnoreLayerCollision(3, 8, false);
            playerMovement.externalForce = false;
            transform.position = respawnPoint;
        }
        else
        {
            return;
        }
    }
}
