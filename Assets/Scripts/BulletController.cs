using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Vector2 dir;

    private Rigidbody2D rb;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dir * bulletSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            if(impactEffect) Instantiate(impactEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
