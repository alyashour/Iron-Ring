using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class RingBossBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject ringDropPrefab;

    private SceneInitialization sceneinit;

    // Boss attributes
    public float enemyHealth = 100f;
    public float enemyDamage = 50f;
    private float enemySpeed = 1f;
    private float enemyKnockBack = 10f;

    // Hit flag fields
    private bool isBeingHit = false;
    private float hitTimeStamp;

    // Flag for if the boss is alive
    private bool isAlive = true;

    private Vector3 deathPos;
    
    private void Start()
    {
        sceneinit = GameObject.Find("SceneController").GetComponent<SceneInitialization>();
    }

    private void Update()
    {
        if (sceneinit.sceneState == 2)
        {
            if (!isBeingHit)
            {
                Move();
            } else
            {
                if (Time.time - hitTimeStamp > 1f || rb.velocity.magnitude < 0.25f)
                {
                    isBeingHit = false;
                }
            }
        }

        if (enemyHealth <= 0 && isAlive)
        {
            deathPos = transform.position;
            isAlive = false;
            Destroy(gameObject, 0.25f);
            Death();
        }
        //print(enemyHealth);
    }

    private void OnHit()
    {
        isBeingHit = true;
        hitTimeStamp = Time.time;

        enemyHealth -= PlayerAttributes.PlayerDamage;
        sr.color = Color.white;

        GetKnockback();
    }

    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = dir.normalized * enemySpeed;
        // Reset the color
        sr.color = Color.red;
    }

    private void GetKnockback()
    {
        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = -dir.normalized * enemyKnockBack;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position.z == 0.05f)
        {
            isBeingHit = true;
            hitTimeStamp = Time.time;
            enemyHealth -= 5f;
            sr.color = Color.white;
        }
    }

    private void Death()
    {
        int num = 32;
        for (int i = 0; i < num; i++)
        {
            float angle = i * (360f / num);
            Quaternion q = Quaternion.Euler(0, angle, 0);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 2.01f);
            GameObject a = Instantiate(particlePrefab, pos, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().rotation = angle;
        }
        Instantiate(ringDropPrefab, deathPos, Quaternion.identity);
        Destroy(GameObject.Find("HealthBarDisplay(Clone)"));
    }
}