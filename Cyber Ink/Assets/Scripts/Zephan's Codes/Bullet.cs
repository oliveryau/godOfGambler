using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    [SerializeField] private int bulletDamage;
    public RangedEnemy enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        rb.velocity = transform.right * speed;
        
        Physics2D.IgnoreLayerCollision(13, 3);
        Physics2D.IgnoreLayerCollision(13, 11);
        Physics2D.IgnoreLayerCollision(13, 12);
        Physics2D.IgnoreLayerCollision(13, 9);
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Terrain Wall")
        {
            Destroy(gameObject);
        }
    }
}
