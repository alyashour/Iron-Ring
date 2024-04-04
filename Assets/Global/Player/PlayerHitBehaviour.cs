using Global.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Player behaviour for when they're hit
// Author: Aiden

public class PlayerHitBehaviour : MonoBehaviour
{
    // Component references
    private Rigidbody2D _rb;

    // Reference to the collided object
    private GameObject _collidedObject;
    private GameObject _player;
    private SpriteRenderer _spriteRenderer;

    // Timer related fields - set the amount of time between hits (invulnerabilty time)
    private float cooldownTime = 0.3f;
    private float lastHitTime = 0;
    private float timeSinceHit = 0;

    // True if the player is being knocked back
    private bool beingKnockedBack = false;
    private float knockbackLength = 0.2f;

    [SerializeField] float damagePerHit = 10;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (beingKnockedBack && timeSinceHit > knockbackLength)
        {
            beingKnockedBack = false;
            _player.GetComponent<PlayerMovement>().lockMovement = false;
        }
        if (beingKnockedBack)
        {
            timeSinceHit += Time.deltaTime;
        } else
        {
            timeSinceHit = 0;
        }

        if (timeSinceHit > 0.1f)
        {
            _spriteRenderer.color = PlayerAttributes.PlayerColor;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the collision was with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // Check there has been the alloted cooldown time between hits
            if (Time.time - lastHitTime > cooldownTime)
            {

                // Store the object collided with
                _collidedObject = collision.gameObject;

                // Damage the player
                if (collision.gameObject.name == "Golem Boss")
                {
                    PlayerAttributes.PlayerHealth -= 5;
                } else
                {
                    PlayerAttributes.PlayerHealth -= damagePerHit;
                }
                
                GetKnockback(_collidedObject);

                // Update the hit time
                lastHitTime = Time.time;

                // Change the sprite colour
                _spriteRenderer.color = Color.red;
            }
        }
    }

    // Gets the knockback for the player when called
    private void GetKnockback(GameObject o)
    {
        beingKnockedBack = true;
        _player.GetComponent<PlayerMovement>().lockMovement = true;

        Vector3 dir = transform.position - o.transform.position;

        if (o.name == "Golem Boss") {
            _rb.velocity = dir.normalized * 100f * Time.deltaTime;
        } else
        {
            _rb.velocity = dir.normalized * PlayerAttributes.PlayerKnockback * Time.deltaTime;
        }
    }
}