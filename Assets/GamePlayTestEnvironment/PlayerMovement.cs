using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Intended for use as a component on the player game object
 * Author: Aiden
*/

public class PlayerMovement : MonoBehaviour
{

    // Component references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Store the current user input, and the current smoothed input
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    // Place holder struct ref used in smooth dampening
    private Vector2 movementInputSmoothedVelocity;



    // The player speed
    [SerializeField] float speed = 1.75f;
    // The amount of time it takes to smooth the movement (basically how slippy the movement feels)
    private float smoothTime = 0.05f;





    private void FixedUpdate()
    {
        // Moves the player
        Move();

        // Flips the sprite axis based on the input
        Animate();

    }


    // Applies the player movement, based on the user input
    private void Move()
    {
        // Calculates the new smoothed input value based on the current value and the target value
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothedVelocity, smoothTime);
        rb.velocity = smoothedMovementInput * speed;
    }


    // Flips the sprite axis based on the user input to face the player in the direction they're moving
    private void Animate()
    {

        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }


    }


    // Gets the user input for player movement
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

}