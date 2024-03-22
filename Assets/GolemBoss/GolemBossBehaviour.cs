using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private GameObject player;

    [SerializeField] float speed = 0.25f;

    private bool hit = false;
    private bool split50 = false;
    private bool split25 = false;
    private bool split10 = false;

    public float health = 20;
    private float maxHealth;

    [SerializeField] GameObject healthBarPrefab;
    private GameObject healthBarInstance;

    // reference to prefab
    [SerializeField] GameObject miniGolemPrefab;


    private void Start()
    {
        player = GameObject.Find("Player");
        maxHealth = health;
    }

    public void Update()
    {
        if (!hit)
        {
            MoveToPlayer();
        }

        if (hit && rb.velocity.magnitude <= 0.25f && health > 0)
        {
            hit = false;
        }

        if (healthBarInstance != null)
        {
            healthBarInstance.transform.Find("Health").localScale = new Vector3(Mathf.Clamp(health, 0, 1), 1, 1);
        }


        if (health <= 0)
        {
            hit = true;
            DestroyMiniGolems();
            Destroy(gameObject, 0.25f);
        }

        // Check for health thresholds for splitting
        float healthPercentage = health / maxHealth;
        
        if (!split50 && healthPercentage <= 0.5f)
        {
            Debug.Log("split");
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

    public void MoveToPlayer()
    {

        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = direction.normalized * speed;

    }


    public void OnHit(Vector3 playerPos)
    {
        hit = true;
        health -= 0.34f;
       
        if (healthBarInstance == null)
        {
            Vector3 healthBarPosition = new Vector3(0, 0.1f, -0.1f);
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + healthBarPosition, Quaternion.identity, gameObject.transform);
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
}
