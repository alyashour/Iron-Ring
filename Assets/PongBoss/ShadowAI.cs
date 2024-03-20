using UnityEngine;

namespace PongBoss
{
    /*
        This pong AI follows the ball limited by a constant velocity factor.
        Author: Aly
    */
    
    public class ShadowAI : IPongAI
    {
        private readonly float _velocity;
        private readonly float _horizontalTolerance;

        public ShadowAI(float velocity, float xTol)
        {
            _velocity = velocity;
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
            // if the paddle is close enough, don't do anything
            if (Mathf.Abs(paddlePosition.x - ballPosition.x) < _horizontalTolerance) return 0f;
        
            // else, accelerate towards it
            if (paddlePosition.x < ballPosition.x) return _velocity;
            return -_velocity;
        }

        public void InitBounds(Rect bounds)
        {
            // not used for this implementation
        }
    }
}
