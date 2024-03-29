using Global.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossBehaviour : MonoBehaviour
{
    private bool split50 = false;
    private bool split25 = false;
    private bool split10 = false;

    // reference to prefab
    [SerializeField] GameObject miniGolemPrefab;
    [SerializeField] GameObject portalPrefab;
    [SerializeField] GameObject pathwayRocks;

    private float maxHealth;

    // Component, prefab, and game object references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject enemyHealthBarPrefab;
    public GameObject player;

    // Enemy attributes
    public float enemyHealth = 100f;
    public float enemyDamage = 10f;
    public float enemySpeed = 2f;
    public float enemyKnockBack = 10f;

    // Hit flag fields
    private bool isBeingHit = false;
    private float hitTimeStamp;

    // Flag for if the enemy is alive
    private bool isAlive = true;

    // Reference to the health bar beloning to this enemy
    private GameObject healthBarInstance;
    private Transform healthBar;

    // reference to lightning bolt prefab
    public GameObject lightningInstance;

    public void Start()
    {
        // Assigns the player reference
        player = GameObject.Find("Player");
        maxHealth = enemyHealth;
    }

    public void Update()
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
                float clampHealth = Mathf.Clamp(enemyHealth/500, 0, 500);
                healthBar.localScale = new Vector3(clampHealth , 1, 1);
            }

            // Stops enemy behaviour and destroys the enemy game object if the health falls below 0
            if (enemyHealth <= 0)
            {
                isAlive = false;
                DestroyMiniGolems();
                Destroy(gameObject, 0.25f);
                Destroy(pathwayRocks, 0.25f);
            }
        }
       
        // Check for health thresholds for splitting
        float healthPercentage = enemyHealth / maxHealth;

        if (!split50 && healthPercentage <= 0.5f)
        {
            Split();
            split50 = true;
        }
        else if (!split25 && healthPercentage <= 0.25f)
        {
            Split();
            split25 = true;
        }
        else if (!split10 && healthPercentage <= 0.1f)
        {
            Split();
            split10 = true;
        }
    }

    // mini golems split off golem boss
    public void Split()
    {
        Vector3 spawnPosition;

        spawnPosition = transform.position + new Vector3(-0.2f, 0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(0, 0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(0.2f, 0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(-0.2f, 0, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(0.2f, 0, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(-0.2f, -0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(0, -0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);

        spawnPosition = transform.position + new Vector3(0.2f, -0.2f, 0);
        Instantiate(miniGolemPrefab, spawnPosition, Quaternion.identity);
    }

    // destroy remaining mini golems
    public void DestroyMiniGolems()
    {
        // Find all instances of the prefab in the scene
        GameObject[] instances = GameObject.FindGameObjectsWithTag(miniGolemPrefab.tag);

        // Loop through each instance and destroy it
        foreach (GameObject instance in instances)
        {
            Destroy(instance, 0.25f);
        }
    }

    private void OnHit()
    {
        // Sets the flag that the enemy is now being hit and records the time of the hit
        isBeingHit = true;
        hitTimeStamp = Time.time;
        // Updates the enemy health to factor in the hit damage
        enemyHealth -= PlayerAttributes.PlayerDamage;

        // Assigns a health bar after the enemy is hit
        if (healthBarInstance == null)
        {
            // Just the offset from the enemy to have the bar display above the enemy's head
            Vector3 healthBarPosition = new Vector3(0, 0.1f, -0.1f);
            healthBarInstance = Instantiate(enemyHealthBarPrefab, transform.position + healthBarPosition, Quaternion.identity, gameObject.transform);
            healthBar = healthBarInstance.transform.Find("Health");
        }

        // 10% chance of lightning
        int randomNumber = UnityEngine.Random.Range(1, 11);
        if (randomNumber == 1) StrikeLightning();
    }

    // Moves the enemy toward the player - slows down when close
    private void MoveToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * enemySpeed;
    }

    public void StrikeLightning()
    {
        // Instantiate lightning effect at player's position
        GameObject lightningEffect = Instantiate(lightningInstance, player.transform.position + new Vector3(0, 1.7595f, 0), Quaternion.identity);

        // Deal damage to the player
        PlayerAttributes.PlayerHealth -= 10;

        // Destroy lightning effect after a certain duration
        Destroy(lightningEffect, 0.5f);
    }
}
