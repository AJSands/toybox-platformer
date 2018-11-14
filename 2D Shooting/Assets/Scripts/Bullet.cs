using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 40;
    public float speed = 20f;
    public float lifetime = 1f; // Time in seconds before bullet is automatically destroyed.
    //public bool explodeOnMiss = false;
    //public bool piercesEntities = false;
    //public bool piercesWalls = false;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Awake()
    {
        //TODO: Object pooling.
        Destroy(gameObject, lifetime);

        //TODO: Explode on miss.
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //TODO: System for choosing collision targets depending on whether bullet was fired by player or enemy.
        //TODO: Piercing.
        if (hitInfo.gameObject.layer != 8 &&
            hitInfo.gameObject.layer != 10 &&
            hitInfo.gameObject.layer != 11)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //TODO: Object pooling.
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
