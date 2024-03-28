using UnityEngine;
using UnityEngine.InputSystem;

/*
    Intended for use as a component on the player game object
    Author: Aiden, Aly
*/

namespace Global.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Component references
        private Animator _animator;
        private Rigidbody2D _rb;
        private AttackBehaviour _attackBehaviour;

        // Store the current user input, and the current smoothed input
        private Vector2 _movementInput;
        private Vector2 _smoothedMovementInput;

        // Place holder struct ref used in smooth dampening
        private Vector2 _movementInputSmoothedVelocity;

        // The player speed
        [SerializeField] private float speed = 1.75f;

        // The amount of time it takes to smooth the movement (basically how slippy the movement feels)
        private const float SmoothTime = 0.05f;

        private float _lastAttackTime = 0f;

        public AttackBehaviour.PlayerDirection playerDirection;

        public bool lockMovement = false;

        // todo: make this value influence the animation speed!
        [SerializeField] private float attackCooldown = 0.3f; // in seconds
        private bool _isAttacking; // do not use, use the IsAttacking property instead

        public bool IsAttacking
        {
            get => _isAttacking;

            set
            {
                _isAttacking = value;
                _animator.SetBool("isAttacking", value);
                if (value)
                {
                    _animator.SetTrigger("Attack");
                    _lastAttackTime = Time.time;
                }
            }
        }

        private void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _attackBehaviour = GameObject.Find("SwordHitbox").GetComponent<AttackBehaviour>();
        }
        
        private void FixedUpdate()
        {
            // Can't move if you're dead
            if (PlayerAttributes.Alive)
            {
                if (!lockMovement)
                {
                    // Moves the player
                    Move();
                }
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
            if (Time.time - _lastAttackTime > attackCooldown)
            {
                IsAttacking = false;
                _attackBehaviour.StopAttack();
            }
        }

        /**
         * Returns the component with the highest absolute magnitude.
         */
        public char MaxComponent(Vector2 vec)
        {
            
            if (Mathf.Abs(vec.x) == Mathf.Abs(vec.y))
            {
                return 'n'; // n for neither since they're equal
            }
            else if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
            {
                return 'x';
            }
            else // it is greater in y
            {
                return 'y';
            }
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
            
            // update the direction
            var maxAxis = MaxComponent(_rb.velocity);

            if (maxAxis == 'x')
            {
                playerDirection = _rb.velocity.x > 0 ? // if value is positive
                    AttackBehaviour.PlayerDirection.Right : AttackBehaviour.PlayerDirection.Left; // right else left
            }
            else if (maxAxis == 'y')
            {
                playerDirection = _rb.velocity.y > 0 ? // if value is positive,
                    AttackBehaviour.PlayerDirection.Up : AttackBehaviour.PlayerDirection.Down; // up else down
            } // else dont do anything
        }

        // Gets the user input to update the player movement vector
        private void OnMove(InputValue inputValue)
        {
            // Again - can't move if you're dead
            if (PlayerAttributes.Alive)
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
            
        }

        private void OnFire(InputValue inputValue)
        {
            // Can't attack if you're dead
            if (PlayerAttributes.Alive)
            {
                // if already attacking
                if (IsAttacking)
                {
                    // do nothing
                }
                else
                {
                    // not already attacking
                    IsAttacking = true;
                    _attackBehaviour.direction = playerDirection;
                    _attackBehaviour.Attack();

                }
            }
        }
    }
}