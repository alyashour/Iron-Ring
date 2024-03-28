using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deals with the logic for when the player dies
// Author: Aiden

public class PlayerDeathBehaviour : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite finalDeathSprite;

    private float timeSinceDeath = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.SetBool("Alive", true);
    }

    private void Update()
    {
        if (PlayerAttributes.PlayerHealth <= 0 && PlayerAttributes.Alive)
        {
            PlayerAttributes.Alive = false;
            _animator.SetBool("Alive", false);
            _animator.Play("Player_Die");
        }

        if (timeSinceDeath > 0.3f)
        {
            // Sets the sprite to the last frame of the death animation and removes the animator so it stops playing animations
            _spriteRenderer.sprite = finalDeathSprite;
            gameObject.GetComponent<Animator>().enabled = false;
        }

        if (!PlayerAttributes.Alive)
        {
            timeSinceDeath += Time.deltaTime;
        }
    }
}