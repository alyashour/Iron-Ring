using UnityEngine;
using UnityEngine.InputSystem;

/*
    Intended for use as a component on the player game object
    Author: Aiden
*/

namespace Global.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Component references
        private Animator _animator;
        private Rigidbody2D _rb;

        // Store the current user input, and the current smoothed input
        private Vector2 _movementInput;
        private Vector2 _smoothedMovementInput;

        // Place holder struct ref used in smooth dampening
        private Vector2 _movementInputSmoothedVelocity;

        // The player speed
        [SerializeField] private float speed = 1.75f;

        // The amount of time it takes to smooth the movement (basically how slippy the movement feels)
        private const float SmoothTime = 0.05f;

        private void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
        }
        
        private void FixedUpdate()
        {
            // Moves the player
            Move();
        }


        // Applies the player movement, based on the user input
        private void Move()
        {
            // Calculates the new smoothed input value based on the current value and the target value
            _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput,
                ref _movementInputSmoothedVelocity, SmoothTime);

            // pull the movement input down to 0 if very close to avoid floating point shenanigans
            _smoothedMovementInput = new Vector2(
                Mathf.Abs(_smoothedMovementInput.x) < 0.2 ? 0f : _smoothedMovementInput.x,
                Mathf.Abs(_smoothedMovementInput.y) < 0.2 ? 0f : _smoothedMovementInput.y
            );
            
            // calculate and update velocity
            _rb.velocity = _smoothedMovementInput * speed;
            
            // update animator
            _animator.SetFloat("hSpeed", _rb.velocity.x);
            _animator.SetFloat("vSpeed", _rb.velocity.y);
        }

        // Gets the user input to update the player movement vector
        private void OnMove(InputValue inputValue)
        {
            var newMovementInput = inputValue.Get<Vector2>();

            if (newMovementInput.magnitude < 0.05) // should be a very small number (bcuz floats)
            {
                _animator.SetBool("isMoving", false);
                newMovementInput = Vector2.zero;
            }
            else
            {
                // the player is moving
                _animator.SetBool("isMoving", true);
                
                var flip = newMovementInput.x < 0;
                
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, flip ? 180f : 0f));
            }

            _movementInput = newMovementInput;
        }

        private void OnFire(InputValue inputValue)
        {
            _animator.SetTrigger("Attack");
        }
    }
}