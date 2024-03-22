using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolemBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private GameObject player;

    [SerializeField] float speed = 1f;

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
        
        MoveToPlayer();
        

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
        if (healthBarInstance == null)
        {
            Vector3 healthBarPosition = new Vector3(0, 0.1f, -0.1f);
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + healthBarPosition, Quaternion.identity, gameObject.transform);
        }

    }
}
