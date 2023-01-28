using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    [Header("Timers")]
    [SerializeField] private float activationDelay = 2f;
    [SerializeField] private float activeTime = 2f;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool triggered; //When turret gets triggered
    private bool active; //When turret is active and can hurt player

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(ActivateTurret());
            }

            if (active)
            {
                collision.GetComponent<PlayerLife>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateTurret()
    {
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
    }
}
