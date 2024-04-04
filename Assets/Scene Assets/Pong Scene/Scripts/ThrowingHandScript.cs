using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scene_Assets.Pong_Scene.Scripts
{
    /*
     * Manages the pong game's throwing hands.
     * Author: Aly
     */
    public class ThrowingHandScript : MonoBehaviour
    {
        private float _startTime;
        private float _animationLength;
        private bool _isHoldingObject; // by default is false
        private ProjectileController _heldProjectile;
        private Vector2 _heldProjectileVelocity;

        [SerializeField] private float dx;
        [SerializeField] private Animator animator;
        [SerializeField] private float roundingError;
        [FormerlySerializedAs("speedUpMultiplier")] [SerializeField] private float directionChangeBias = 1.1f;
    
        // Start is called before the first frame update
        void Start()
        {
            _startTime = Time.time;
            _animationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
        }

        // Update is called once per frame
        void Update()
        {
            var animationLifetime = Time.time - _startTime;
            if (animationLifetime > _animationLength - roundingError)
            {
                if (!_isHoldingObject)
                {
                    // MAJOR error, this should never happen
                    Debug.LogError("Object isn't holding anything! No collision detected...");
                    Destroy(gameObject);
                    
                    // reset the animation timer
                    _startTime = Time.time;
                }

                ThrowProjectile();
                Destroy(gameObject);
            }
        }

        public void Catch(ProjectileController projectileController)
        {
            _isHoldingObject = true;
            _heldProjectile = projectileController;
            _heldProjectileVelocity = _heldProjectile.Rb.velocity;
        
            // freeze the other object
            _heldProjectile.Freeze();
        }

        private void ThrowProjectile()
        {
            // unfreeze the projectile
            _heldProjectile.UnFreeze();
            
            // then pick a new projectile velocity
            _heldProjectile.Rb.velocity = GenerateNewVelocity();
            
            // update projectile flag
            _heldProjectile.isCaught = false;
        }

        private Vector2 GenerateNewVelocity()
        {
            // a good base velocity is one that is reflected across the x-axis
            // also it is a good idea to make it slightly faster vertically and slightly slower horizontally
            // to avoid balls getting stuck bouncing back and forth between the walls
            var newDirection = new Vector2(
                -_heldProjectileVelocity.x / directionChangeBias, // flip and shrink horizontal
                _heldProjectileVelocity.y * directionChangeBias   // make faster vertical
            ).normalized;
            var newVelocity = newDirection * _heldProjectileVelocity.magnitude; // to ensure the same magnitude
            
            // sometimes however, if the player is very near the edge of the stage
            // even the centered velocity results in a trajectory that is outside the range of projectile minX and maxX.
            // if so, we should clamp the velocity to ensure it does actually reach the middle of the stage
            
            // first, calculate the projectedX position (let the current position be p0 and the final, intersecting position be p1)
            Vector2 currentPosition = _heldProjectile.Rb.position;
            var finalY = _heldProjectileVelocity.y < 0 ? _heldProjectile.MinY : _heldProjectile.MaxY;
            var projectedX = currentPosition.x + ((_heldProjectileVelocity.x / _heldProjectileVelocity.y) * (currentPosition.y - finalY));

            // then clamp the vector if needed
            if (projectedX < _heldProjectile.MinX || projectedX > _heldProjectile.MaxX)
            {
                Vector2 p = new Vector2(
                    projectedX < _heldProjectile.MinX ? _heldProjectile.MinX + dx : _heldProjectile.MaxX - dx,
                    finalY
                );
                var directionVector = (p - currentPosition).normalized;
                newVelocity = directionVector * _heldProjectileVelocity.magnitude;
            }
            
            return newVelocity;
        }
    }
}
