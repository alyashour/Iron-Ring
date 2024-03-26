using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this as a component to a gameobject you want to have a knockback mechanic when hit by the player
// Author: Aiden

public class EnemyKnockback : MonoBehaviour
{
    // Component references
    [SerializeField] Rigidbody2D rb;
    private GameObject _player;

    // The amount of knockback to provide
    [SerializeField] float enemyKnockBack = 10f;

    // Hit flag fields
    private bool _isBeingHit = false;
    private float _hitTimeStamp;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (_isBeingHit)
        {
            if (Time.time - _hitTimeStamp > 1f || rb.velocity.magnitude < 0.25f)
            {
                _isBeingHit = false;
            }
        }
    }

    private void OnHit()
    {
        _isBeingHit = true;
        _hitTimeStamp = Time.time;
        GetKnockback();
    }

    private void GetKnockback()
    {
        Vector3 dir = _player.transform.position - transform.position;
        rb.velocity = -dir.normalized * enemyKnockBack;
    }
}