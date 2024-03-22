using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Script references
    [SerializeField] AttackBehaviour attackBehaviour;

    // Holds the player input for direction
    private Vector2 movementInput;

    // Holds the direction the player is facing
    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    private Direction direction = Direction.Down;

    // Flag for if the player is moving
    private bool isMoving;

    // The current animation state of the player
    private int state = 0;

    // Flag for if the player is attacking
    private bool isAttacking = false;

    // Fields for animation timings
    private float combatStartTime;
    private float attackDuration = 0.5f;

    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void Update()
    {
        // Resets the attacking flag after the animation has completed
        if (Time.time - combatStartTime > attackDuration)
        {
            isAttacking = false;
        }

        // Gets the animations when the player is not attacking
        if (!isAttacking)
        {
            GetAnimation();

            // Disables the hit detection from attacks, when the player is not attacking
            attackBehaviour.StopAttack();
        }
    }

    // Plays the correct animation based on a calculated state value (only called when the player is not attacking)
    private void GetAnimation()
    {
        // Determines if the player is moving or not
        if (rb.velocity.magnitude < 0.25f)
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
                direction = Direction.Right;
            }
            else if (movementInput.x == -1)
            {
                direction = Direction.Left;
            }
        }
        else if (Mathf.Abs(movementInput.x) < Mathf.Abs(movementInput.y))
        {
            if (movementInput.y == 1)
            {
                direction = Direction.Up;
            }
            else if (movementInput.y == -1)
            {
                direction = Direction.Down;
            }
        }

        // Determines the proper animation state based on if the player is moving, and what direction they're facing
        if (!isMoving)
        {
            // Select idle animation based on direction
            switch (direction)
            {
                case Direction.Up:
                    state = 2;
                    break;
                case Direction.Down:
                    state = 0;
                    break;
                case Direction.Right:
                    state = 1;
                    spriteRenderer.flipX = false;
                    break;
                case Direction.Left:
                    state = 1;
                    spriteRenderer.flipX = true;
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case Direction.Up:
                    state = 5;
                    break;
                case Direction.Down:
                    state = 3;
                    break;
                case Direction.Right:
                    state = 4;
                    spriteRenderer.flipX = false;
                    break;
                case Direction.Left:
                    state = 4;
                    spriteRenderer.flipX = true;
                    break;
            }
        }

        // Sets the animator value based on the updated state selection
        animator.SetInteger("NextState", state);
    }

    // Plays the correct attack animation based on the direction value when called
    private void PlayAttackAnimation()
    {
        switch (direction)
        {
            case Direction.Up:
                animator.Play("Player_Attack_B");
                state = 8;

                attackBehaviour.direction = AttackBehaviour.PlayerDirection.Up;
                break;
            case Direction.Down:
                animator.Play("Player_Attack_F");
                state = 6;

                attackBehaviour.direction = AttackBehaviour.PlayerDirection.Down;
                break;
            case Direction.Left:
                animator.Play("Player_Attack_S");
                state = 7;
                spriteRenderer.flipX = true;

                attackBehaviour.direction = AttackBehaviour.PlayerDirection.Left;
                break;
            case Direction.Right:
                animator.Play("Player_Attack_S");
                state = 7;
                spriteRenderer.flipX = false;

                attackBehaviour.direction = AttackBehaviour.PlayerDirection.Right;
                break;
        }

        // Plays the state animation
        animator.SetInteger("NextState", state);


        // Sets the collision detection to the correct size and position; sets it as active
        attackBehaviour.Attack();
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
        if (isAttacking)
        {
            return;
        }

        // Updates the time the attack animation started
        combatStartTime = Time.time;
        // Sets the appropriate flags
        isAttacking = true;

        // Determines and plays the attack animation
        PlayAttackAnimation();
    }
}