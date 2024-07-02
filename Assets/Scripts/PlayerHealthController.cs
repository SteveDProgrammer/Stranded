using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    private void Awake()
    {
        if(instance == null) 
        {   
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); 
        // Destroying `this` will only Destroy the PlayerHealthController Script, 
        // so use gameObject instead
    }

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;

    public float invincibilityDuration;
    private float invincibilityCounter;

    public float flashDuration;
    private float flashCounter;

    public List<SpriteRenderer> playerSprites;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if(flashCounter <= 0)
            {
                playerSprites.ForEach(sprite => sprite.enabled = !sprite.enabled);
                flashCounter = flashDuration;
            }

            if(invincibilityCounter <= 0)
            {
                playerSprites.ForEach(sprite => sprite.enabled = true);
                flashCounter = 0;
            }
        }
    }

    public void DamagePlayer(int damageAmt)
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth = Mathf.Max(currentHealth-damageAmt, 0);
            if(currentHealth <= 0)
            {
                //gameObject.SetActive(false);
                RespawnManager.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityDuration;
            }
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
