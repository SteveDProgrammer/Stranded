using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthBoostAmt;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthController.instance.HealPlayer(healthBoostAmt);

        if(pickupEffect)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
