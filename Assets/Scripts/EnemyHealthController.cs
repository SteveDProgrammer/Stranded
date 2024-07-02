using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 3;
    public GameObject deathEffect;
    
    public void DamageEnemy(int damageAmt)
    {
        totalHealth -= damageAmt;
        if(totalHealth <= 0)
        {
            if(deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
