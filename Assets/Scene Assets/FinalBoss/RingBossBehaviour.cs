using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class RingBossBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;

    [SerializeField] GameObject enemyHealthBarPrefab;
    private GameObject healthBarInstance;
    private Transform healthBar;

    private SceneInitialization sceneinit;

    // Boss attributes
    private float enemyHealth = 100f;
    public float enemyDamage = 50f;
    private float enemySpeed = 0.25f;
    private float enemyKnockBack = 10f;

    // Hit flag fields
    private bool isBeingHit = false;
    private float hitTimeStamp;

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
    }

    private void OnHit()
    {
        isBeingHit = true;
        hitTimeStamp = Time.time;

        enemyHealth -= PlayerAttributes.PlayerDamage;

        GetKnockback();
    }

    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = dir.normalized * enemySpeed;
    }

    private void GetKnockback()
    {
        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = -dir.normalized * enemyKnockBack;
    }
}