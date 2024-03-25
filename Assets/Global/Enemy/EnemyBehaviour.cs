using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Default enemy behaviour script
// Author: Aiden

public class EnemyBehaviour : MonoBehaviour
{
    // Component, prefab, and game object references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject enemyHealthBarPrefab;
    private GameObject player;
    
    // Enemy attributes
    private float enemyHealth = 100f;
    public float enemyDamage = 10f;
    private float enemySpeed = 2f;
    private float enemyKnockBack = 10f;

    // Hit flag fields
    private bool isBeingHit = false;
    private float hitTimeStamp;

    // Flag for if the enemy is alive
    private bool isAlive = true;

    // Reference to the health bar beloning to this enemy
    private GameObject healthBarInstance;
    private Transform healthBar;

    private void Start()
    {
        // Assigns the player reference
        player = GameObject.Find("Player");        
    }

    private void Update()
    {
        if (isAlive)
        {
            // Moves toward the player if the enemy is not being hit currently
            if (!isBeingHit)
            {
                MoveToPlayer();
            }
            else
            {
                // Resets the isBeingHit flag if the time or velocity reach certain values
                if (Time.time - hitTimeStamp > 1f || rb.velocity.magnitude < 0.25f)
                {
                    isBeingHit = false;
                }
            }

            // Updates the health bar size based on the health value
            if (healthBarInstance != null)
            {
                float clampHealth = Mathf.Clamp(enemyHealth, 0, 100);
                healthBar.localScale = new Vector3(clampHealth * 0.01f, 1, 1);
            }

            // Stops enemy behaviour and destroys the enemy game object if the health falls below 0
            if (enemyHealth <= 0)
            {
                isAlive = false;
                Destroy(gameObject, 0.25f);
            }
        }
    }
    
    private void OnHit()
    {
        // Sets the flag that the enemy is now being hit and records the time of the hit
        isBeingHit = true;
        hitTimeStamp = Time.time;
        // Updates the enemy health to factor in the hit damage
        enemyHealth -= PlayerAttributes.PlayerDamage;
        // Knocks the enemy back
        GetKnockback();
        
        // Assigns a health bar after the enemy is hit
        if (healthBarInstance == null)
        {
            // Just the offset from the enemy to have the bar display above the enemy's head
            Vector3 healthBarPosition = new Vector3(0, 0.1f, -0.1f);
            healthBarInstance = Instantiate(enemyHealthBarPrefab, transform.position + healthBarPosition, Quaternion.identity, gameObject.transform);
            healthBar = healthBarInstance.transform.Find("Health");
        }
    }

    // Moves the enemy toward the player - slows down when close
    private void MoveToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * enemySpeed;
    }

    // Changes the velocity of the enemy to "knock" it back away from the player after being hit
    private void GetKnockback()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = -direction.normalized * enemyKnockBack;
    }

}