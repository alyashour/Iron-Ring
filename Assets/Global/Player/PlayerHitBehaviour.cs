using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBehaviour : MonoBehaviour
{
    // Component references
    [SerializeField] Rigidbody2D _rb;

    // Reference to the collided object
    private GameObject _collidedObject;

    // Timer related fields - set the amount of time between hits (invulnerabilty time)
    [SerializeField] float cooldownTime = 1f;
    private float lastHitTime = 0;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the collision was with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // Check there has been the alloted cooldown time between hits
            if (Time.time - lastHitTime > cooldownTime)
            {
                try
                {
                    // Store the object collided with
                    _collidedObject = collision.gameObject;

                    // Damage the player
                    PlayerAttributes.PlayerHealth -= _collidedObject.GetComponent<EnemyBehaviour>().enemyDamage;
                    GetKnockback(_collidedObject);
                }
                catch
                { //ignore
                }
            }

            // Update the hit time
            lastHitTime = Time.time;
        }
    }

    // Gets the knockback for the player when called
    private void GetKnockback(GameObject o)
    {
        Vector3 dir = transform.position - o.transform.position;
        _rb.velocity = -dir.normalized * PlayerAttributes.PlayerKnockback * 1000;
    }
}