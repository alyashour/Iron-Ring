using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenEnBehaviour : MonoBehaviour
{
    // Component, prefab, and game object references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject enemyHealthBarPrefab;
    private GameObject player;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Enemy attributes
    public float enemyHealth = 25f;
    public float enemyMaxHealth = 25f;
    public float enemySpeed = 0.25f;
    public float enemyKnockBack = 100f;

    // Hit flag fields
    private bool isBeingHit = false;
    private float hitTimeStamp;

    // Flag for if the enemy is alive
    private bool isAlive = true;

    // Reference to the health bar beloning to this enemy
    private GameObject healthBarInstance;
    private Transform healthBar;

    [SerializeField] GameObject foodDrop;
    [SerializeField] GameObject xpDrop;

    private float startTime;

    private void Start()
    {
        // Assigns the player reference
        player = GameObject.Find("Player");
        spriteRenderer.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        startTime = Time.time;
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
                float clampHealth = Mathf.Clamp(enemyHealth / enemyMaxHealth * 1, 0, 1);
                healthBar.localScale = new Vector3(clampHealth, 1, 1);
            }

            // Stops enemy behaviour and destroys the enemy game object if the health falls below 0
            if (enemyHealth <= 0)
            {
                isAlive = false;
                GolemSpawnBehaviour.numActive--;

                int rand = Random.Range(0, 6);
                if (rand == 1)
                {
                    Instantiate(foodDrop, transform.position, Quaternion.identity);
                }
                print("here");
                Instantiate(xpDrop, transform.position, Quaternion.identity);
                Invoke(nameof(Drop), 0.5f);
                SpawnAtPlayer.numActive--;
                Destroy(gameObject, 0.25f);
            }
        }
    }
    private void Drop()
    {
        print("dropping!");
        Instantiate(xpDrop, transform.position, Quaternion.identity);
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
            Vector3 healthBarPosition = new Vector3(0, 0.2f, -0.1f);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn" && Time.time - startTime < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
