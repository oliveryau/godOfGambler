//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Enemy : MonoBehaviour
//{
//    [SerializeField]
//    private int damage = 5;
//    [SerializeField]
//    private float speed = 1.5f;
//    [SerializeField]
//    private EnemyData data;
//    [SerializeField]
//    private Text statusText;

//    private GameObject player;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        SetEnemyValues();
//    }

//    void Update()
//    {
//        Swarm();
//    }

//    private void SetEnemyValues()
//    {
//        GetComponent<Health>().SetHealth(data.hp, data.hp);
//        damage = data.damage;
//        speed = data.speed;
//    }
//    private void Swarm()
//    {
//        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
//    }

//    private void OnTriggerEnter2D(Collider2D collider)
//    {
//        if (collider.CompareTag("Player"))
//        {
//            collider.GetComponent<Health>().Damage(damage);
//            this.GetComponent<Health>().Damage(10000);
//        }

//        if (collider.gameObject.CompareTag("Enemy")) //Check Slows tag
//        {
//            Destroy(collider.gameObject);
//            statusText.text = "Enemy Defeated!";

//        }
//    }
//}
