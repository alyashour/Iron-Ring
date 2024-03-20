using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author - Aiden


public class RockBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    private GameObject player;

    [SerializeField] float speed = 2f;

    private bool hit = false;


    public float health = 1;
    [SerializeField] GameObject healthBarPrefab;
    private GameObject healthBarInstance;


    private void Start()
    {
        player = GameObject.Find("Player");
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
            Destroy(gameObject, 0.25f);
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
        GetKnockback(playerPos);
        if (healthBarInstance == null)
        {
            Vector3 healthBarPosition = new Vector3(0, 0.1f, -0.1f);
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + healthBarPosition, Quaternion.identity, gameObject.transform);
        }
        
    }

    public void GetKnockback(Vector3 playerPos)
    {
        Vector3 direction = playerPos - transform.position;
        rb.velocity = -direction.normalized * 10f;
    }

}