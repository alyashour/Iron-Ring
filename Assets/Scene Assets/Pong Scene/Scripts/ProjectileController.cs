using Unity.VisualScripting;
using UnityEngine;

namespace PongBoss
{
    /*
        Controls the sword projectile in the pong game
        Author: Aly
    */

    public class ProjectileController : MonoBehaviour
    {
        private GameObject _player;
        private GameObject _pong;
        private Rigidbody2D _rb;
        public float speedupMultiplier = 1.1f;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player");
            _pong = GameObject.Find("Pong");
            _rb = GetComponent<Rigidbody2D>();
        }

        private void BounceBack(Vector2 otherVelocity)
        {
            // bounce back at a higher speed
            var vel = _rb.velocity;
            var currentSpeed = vel.magnitude;
            var newSpeed = currentSpeed * speedupMultiplier;

            // pong velocity is clamped to the x axis
            var direction = (otherVelocity + new Vector2(vel.x, -vel.y)).normalized;
            var newVel = direction * newSpeed;

            // assign new velocity
            _rb.velocity = newVel;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.name)
            {
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
                case "SceneBoundary":
                {
                    // reverse horizontal velocity
                    var v = _rb.velocity;
                    var newVelocity = new Vector2(-v.x, v.y);
                    _rb.velocity = newVelocity;
                    break;
                }
                case "Pong":
                {
                    BounceBack(other.GetComponent<Rigidbody2D>().velocity);
                    _pong.GetComponent<PongController>().OnProjectileBounce(gameObject);
                    break;
                }
                case "Player":
                {
                    if (other.GetComponent<PlayerAnimation>().IsAttacking())
                    {
                        BounceBack(other.GetComponent<Rigidbody2D>().velocity);
                    }
                    break;
                }
            }
        }
    }
}
