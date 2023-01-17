using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private Respawn respawn;
    // Start is called before the first frame update

    private void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;
        }
    }
}
