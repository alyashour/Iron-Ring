using UnityEngine;

namespace PongBoss
{
    /*
        Interface for paddle AI controllers
        Author: Aly
    */
    
    public interface IPongAI
    {
        /**
         * This describes in which direction should the paddle move and what direction.
         * A positive value implies moving to the right and a negative value to the left.
         * Value in meters per second
         *
         * the ball may not exist at any time
         * it is important this func handles null for both of these values
         */
        float ComputePaddleVelocity(
            Vector2 paddlePosition, Vector2 paddleVelocity,
            Vector2 ballPosition, Vector2 ballVelocity, 
            Vector2 playerPosition // we can ignore player velocity as the player can change direction immediately
        );

        void InitBounds(Rect bounds);
    }
}