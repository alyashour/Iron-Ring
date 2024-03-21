using System;
using UnityEngine;

namespace PongBoss
{
    /*
        Pong boss controller
        Author: Aly
    */
    
    public class PongController : MonoBehaviour
    {
        public float breakSpeed = 10f; // the speed at which pong will break
        public float paddleAcceleration = 20f;
        public float paddleVelocity = 3f;
        public float paddleXTolerance = 0.3f;
        
        // these values are all to reduce frequent lookups
        private Rigidbody2D _swordRb;
        private GameObject _player;
        private GameObject _levelManager;
        
        private IPongAI _ai; // AI interface that handles logic
        private Rigidbody2D _rb; // this object's rigid body (for ease of use)
        
        
        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _player = GameObject.Find("Player");
            _ai = new ShadowAI(paddleVelocity, paddleXTolerance);
            _levelManager = GameObject.Find("LevelManager");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_swordRb != null)
            {
                // get movement value from the AI
                var vel = _ai.ComputePaddleVelocity(
                    gameObject.transform.position,
                    _rb.velocity,
                    _swordRb.position,
                    _swordRb.velocity,
                    _player.transform.position
                ) * Vector2.right;

                _rb.velocity = vel;
            }
        }
        
        /*
         * This updates the SetSwordRb
         * It must be called by any classes that update the current sword (pong ball).
         * rb may be null.
         */
        public void SetSwordRb(Rigidbody2D rb)
        {
            _swordRb = rb;
        }

        public void OnProjectileBounce(GameObject projectile)
        {
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            // get the normal component of the vector
            float projectileNormalSpeed = projectileRb.velocity.y;
            
            if (Math.Abs(projectileNormalSpeed) > breakSpeed)
            {
                _levelManager.GetComponent<LevelController>().GameOver();
            }
        }
    }
}
