using System;
using Global.Player;
using PongBoss;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scene_Assets.Pong_Scene.Scripts
{
    /*
        Controls the sword projectile in the pong game
        Author: Aly
    */
    public class ProjectileController : MonoBehaviour
    {
        private GameObject _player;
        private GameObject _pong;
        public bool isCaught;
        
        [SerializeField] private GameObject throwerPrefab;
        [SerializeField] private float directionBias = 15f; // the amount the player's direction affects the movement of the projectile
        [SerializeField] private float speedupMultiplier = 1.1f;
        [SerializeField] private float maxHandSpawnDelay = 0.2f; // randomNum * thisMultiplier = dt between hitting the boundary and being caught
        [SerializeField] private float catchPrepTime = 0.2f; // the time before the catch, waiting for the hand to pop up

        public readonly float MinX = -1.5f;
        public readonly float MaxX = 1.5f;
        public readonly float MinY = -1f;
        public readonly float MaxY = 0.7f; // this is significantly smaller cuz the boss never misses
        
        public Rigidbody2D Rb
        {
            get;
            private set;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player");
            _pong = GameObject.Find("Pong");
            Rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // save position and velocity for later
            var pos = transform.position;
            var vel = Rb.velocity;
            
            // veto conditions
            if (isCaught) { return; }
            if (pos.y < MinY || pos.y > MaxY) { return; }
            
            // calculate conditions
            bool leftCondition = vel.x < 0 && pos.x < MinX; // if moving left AND past the left
            bool rightCondition = vel.x > 0 && pos.x > MaxX; // if moving right AND past right

            if (leftCondition || rightCondition)
            {
                isCaught = true;
                Invoke(nameof(InitThrowingHand), Random.value * maxHandSpawnDelay);
            }
        }

        private void BounceBack(Vector2 otherVelocity, float xBias = 0f, float yBias = 0f)
        {
            // bounce back at a higher speed
            var vel = Rb.velocity;
            var currentSpeed = vel.magnitude;
            var newSpeed = currentSpeed * speedupMultiplier;

            // pong velocity is clamped to the x axis
            var direction = (otherVelocity + new Vector2(vel.x, -vel.y)).normalized;
            var newVel = direction * newSpeed;
            
            // add the biases
            newVel.x += xBias;
            newVel.y += yBias;

            // assign new velocity
            Rb.velocity = newVel;
        }

        private void HandleCollision(GameObject other)
        {
            switch (other.name)
            {
                // on collision with the top/bottom of the scene
                case "TopTrigger":
                case "BottomTrigger":
                {
                    // update pong boss
                    PongController pongController = GameObject.Find("Pong").GetComponent<PongController>();
                    pongController.SetSwordRb(null);
                
                    // update player
                    _player.GetComponent<ThrowController>().canThrow = true;
                
                    // destroy this sword object
                    Destroy(gameObject);

                    break;
                }
                case "Pong":
                {
                    // bounce the ball back
                    // I don't use bounce back here because of an issue that sometimes makes the projectile bounce up
                    var vel = Rb.velocity;
                    var currentSpeed = vel.magnitude;
                    var newSpeed = currentSpeed * speedupMultiplier;

                    // pong velocity is clamped to the x axis
                    var direction = (other.GetComponent<Rigidbody2D>().velocity // this is only every horizontal
                                     + new Vector2(vel.x, -Math.Abs(vel.y))) // -Abs is to make sure the vector points down
                        .normalized; // normalize cuz direction
                    var newVel = direction * newSpeed;

                    // assign new velocity
                    Rb.velocity = newVel;
                    
                    // alert pong (for win condition)
                    _pong.GetComponent<PongController>().OnProjectileBounce(gameObject);
                    
                    break;
                }
                case "Player":
                {
                    // check if the player is attacking
                    var playerMovement = other.GetComponent<PlayerMovement>();
                    
                    if (playerMovement.IsAttacking)
                    {
                        // get the direction
                        var direction = playerMovement.playerDirection;
                        var xBias = 0f;
                        var yBias = 0f;
                        
                        switch (direction)
                        {
                            case AttackBehaviour.PlayerDirection.Down:
                                return; // do nothing if the player is facing downwards
                            case AttackBehaviour.PlayerDirection.Left:
                                xBias = -directionBias;
                                break;
                            case AttackBehaviour.PlayerDirection.Right:
                                xBias = directionBias;
                                break;
                            case AttackBehaviour.PlayerDirection.Up:
                                yBias = directionBias;
                                break;
                            default:
                                return; // do nothing
                        }
                        BounceBack(other.GetComponent<Rigidbody2D>().velocity, xBias, yBias);
                    }
                    
                    break;
                }
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            // if its a throwing hand
            if (other.CompareTag("Hand"))
            {
                other.SendMessage("Catch", this); // pass in the projectile controller
            }
            else
            {
                HandleCollision(other.gameObject);
            }
        }

        public void InitThrowingHand()
        {
            // calculate the position to be caught in
            var position3 = transform.position;
            var currentPosition = new Vector2(position3.x, position3.y);
            var position = currentPosition + Rb.velocity * catchPrepTime; // u = s + vt
            
            // create a throwing hand object
            GameObject hand = Instantiate(throwerPrefab, position, Quaternion.identity);
            
            // if the projectile has a negative velocity
            if (Rb.velocity.x < 0)
                hand.GetComponent<SpriteRenderer>().flipX = true; // flip the sprite
            
            // the object reversal is now under the control of the hand.
        }

        public void Freeze()
        {
            Rb.constraints = RigidbodyConstraints2D.FreezePosition;
            gameObject.SetActive(false);
        }

        public void UnFreeze()
        {
            Rb.constraints = RigidbodyConstraints2D.None;
            gameObject.SetActive(true);
        }
    }
}
