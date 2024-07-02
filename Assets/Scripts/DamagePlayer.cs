using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmt = 1;
    
    public bool destroyOnDamage;
    public GameObject destroyEffect;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmt);

        if(destroyOnDamage)
        {
            if(destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);   
            }
            Destroy(gameObject);
        }
    }
}
