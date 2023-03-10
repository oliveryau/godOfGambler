using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    public bool startDestroying = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y && startDestroying == false)
        {
            startDestroying = true;
            Destroy(gameObject, 0.6f);
            anim.SetTrigger("destroying");
            AudioManager.Instance.PlayEffectsOneShot("Disappearing Platform");
            FallPlatformManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));
        }
    }
}
