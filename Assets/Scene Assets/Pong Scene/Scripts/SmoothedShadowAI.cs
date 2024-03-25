using UnityEngine;

namespace PongBoss
{
    /**
     * This AI follows the sword exactly limited by a constant acceleration factor.
     */
    
    public class SmoothedShadowAI : IPongAI
    {
        private readonly float _acceleration;
        private readonly float _horizontalTolerance;

        public SmoothedShadowAI(float acceleration, float xTol)
        {
            _acceleration = acceleration;
            _horizontalTolerance = xTol;
        }
        
        public float ComputePaddleVelocity(
            Vector2 paddlePosition, Vector2 paddleVelocity,
            Vector2 ballPosition, Vector2 ballVelocity, 
            Vector2 playerPosition)
        {
            // if the ball is moving away from us, do nothing
            if (ballVelocity.y < 0f) return paddleVelocity.x;
            
            // else,
            return paddleVelocity.x +
                   ComputePaddleAcceleration(paddlePosition.x, ballPosition.x) * Time.deltaTime;
        }

        private float ComputePaddleAcceleration(float paddleXPos, float ballXPos)
        {
            // if the paddle is close enough, don't do anything
            if (Mathf.Abs(paddleXPos - ballXPos) < _horizontalTolerance) return 0f;
            
            // else, accelerate towards it
            if (paddleXPos < ballXPos) return _acceleration;
            return -_acceleration;
        }

        public void InitBounds(Rect bounds)
        {
            // not used for this implementation
        }
    }
}
