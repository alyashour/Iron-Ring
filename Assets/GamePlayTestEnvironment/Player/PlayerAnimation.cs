using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*
    Intended for use as a component on the player gameobject, determines and sets the proper animations
    Author: Aiden
*/

public class PlayerAnimation : MonoBehaviour
{
    /*
    STATES:

    0 - Player_Idle_F
    1 - Player_Idle_S (flipX = true, false)
    2 - Player_Idle_B

    3 - Player_Walk_F
    4 - Player_Walk_S (flipX = true, false)
    5 - Player_Walk_B

    6 - Player_Attack_F
    7 - Player_Attack_S (flipX = true, false)
    8 - Player_Attack_B

    9 - Player_Die

    */

    // Component references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Holds the player input for direction
    private Vector2 movementInput;

    // Holds the direction the player is facing
    private string direction;

    // Flag for if the player is moving
    private bool isMoving;

    // The current animation state of the player
    private int state = 0;

    // Flag for if the player is attacking
    private bool isAttacking;

    // Fields for animation timings
    private float attackStartTime;
    private float attackDuration = 0.5f;

    private void Update()
    {
        // Resets the attacking flag after the animation has completed
        if (Time.time - attackStartTime > attackDuration)
        {
            isAttacking = false;
        }

        // Gets the animations when the player is not attacking
        if (!isAttacking)
        {
            GetAnimation();
        }
    }

    // Plays the correct animation based on a calculated state value (only called when the player is not attacking)
    private void GetAnimation()
    {
        // Determines if the player is moving or not
        if (rb.velocity.magnitude < 0.5f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        // Determines the direction the player should face, based on the largest input value
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            if (movementInput.x == 1)
            {
                direction = "right";
            }
            else if (movementInput.x == -1)
            {
                direction = "left";
            }
        }
        else if (Mathf.Abs(movementInput.x) < Mathf.Abs(movementInput.y))
        {
            if (movementInput.y == 1)
            {
                direction = "up";
            }
            else if (movementInput.y == -1)
            {
                direction = "down";
            }
        }

        // Determines the proper animation state based on if the player is moving, and what direction they're facing
        if (!isMoving)
        {
            // Select idle animation based on direction
            if (direction == "up")
            {
                // Idle facing back (up)
                state = 2;
            }
            else if (direction == "down")
            {
                // Idle facing forward (down)
                state = 0;
            }
            else if (direction == "right")
            {
                // Idle facing to the side (right)
                state = 1;
                spriteRenderer.flipX = false;
            }
            else if (direction == "left")
            {
                // Idle facing to the side (left)
                state = 1;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            // Select walk animation
            if (direction == "up")
            {
                // Walk back (up)
                state = 5;
            }
            else if (direction == "down")
            {
                // Walk forward (down)
                state = 3;
            }
            else if (direction == "right")
            {
                // Walk to the side (right)
                state = 4;
                spriteRenderer.flipX = false;
            }
            else if (direction == "left")
            {
                // Walk to the side (left)
                state = 4;
                spriteRenderer.flipX = true;
            }
        }

        // Sets the animator value based on the updated state selection
        animator.SetInteger("NextState", state);
    }

    // Plays the correct attack animation based on the direction value when called
    private void PlayAttackAnimation()
    {
        // Select attack animation
        if (direction == "up")
        {
            // Attack back (up)
            animator.Play("Player_Attack_B");
            state = 8;
        }
        else if (direction == "down")
        {
            // Attack forward (down)
            animator.Play("Player_Attack_F");
            state = 6;
        }
        else if (direction == "right")
        {
            // Attack to the side (right)
            animator.Play("Player_Attack_S");
            spriteRenderer.flipX = false;
            state = 7;
        }
        else if (direction == "left")
        {
            // Attack to the side (left)
            animator.Play("Player_Attack_S");
            state = 7;
            spriteRenderer.flipX = true;
        }

        // Plays the state animation
        animator.SetInteger("NextState", state);
    }

    // Gets the user input for player movement
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    // Gets user input for attacking
    private void OnFire(InputValue inputValue)
    {
        // Stops the player from attacking when they already are attacking
        if (!isAttacking)
        {
            // Updates the time the attack animation started
            attackStartTime = Time.time;
            // Sets the attacking flag
            isAttacking = true;

            // Determines and plays the attack animation
            PlayAttackAnimation();
        }
    }
}