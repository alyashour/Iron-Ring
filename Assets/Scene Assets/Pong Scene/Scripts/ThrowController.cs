using PongBoss;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Scene_Assets.Pong_Scene.Scripts
{
    public class ThrowController : MonoBehaviour
    {
        public float defaultSpeed = 1f;
        public bool canThrow = true;
        public GameObject projectile;
        private Rigidbody2D _rb; // the players rigid body

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /**
         * Called by the character input manager when the player uses the throw keybind
         */
        private void OnThrow(InputValue inputValue)
        {
            if (!canThrow) return; // if the player can't throw don't do anything
            
            // calculate where to spawn the sword above the player using an offset
            var playerHalfHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            var swordHalfHeight = projectile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            Vector3 positionOffset = Vector3.up * (playerHalfHeight + swordHalfHeight);
            
            // create the sword object above the player and give it an initial velocity vector
            Rigidbody2D swordRb = Instantiate(projectile, transform.position + positionOffset, Quaternion.identity).GetComponent<Rigidbody2D>();
            swordRb.velocity = new Vector2(_rb.velocity.x, 1).normalized * defaultSpeed;
            canThrow = false; // there should only ever be 1 thrown object
            
            // let the pong controller know there is a new sword on the field
            PongController pongController = GameObject.Find("Pong").GetComponent<PongController>();
            pongController.SetSwordRb(swordRb);
        }
    }
}
