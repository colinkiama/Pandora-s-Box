using Flames.Enum;
using Flames.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;

namespace Flames.Model
{
    public class Player : IPhysicsObject
    {
        public SpriteVisual Visual { get; set; }
        public float PlayerAcceleration { get; set; }
        public float Velocity { get; set; }
        const float MaxVelocity = 12.0f;
        const float AccelerationPerSecond = -4f;


        public Player(Compositor compositor)
        {

            Visual = compositor.CreateSpriteVisual();
            Visual.Brush = compositor.CreateColorBrush(Colors.Purple);
            Visual.Size = new Vector2(40, 40);
        }

        public void Accelerate(int deltaTimeMilliseconds, Direction movementDirection = Direction.None)
        {
            switch (movementDirection)
            {
                case Direction.Left:
                    PlayerAcceleration = -AccelerationPerSecond;
                    break;
                case Direction.Right:
                    PlayerAcceleration = AccelerationPerSecond;
                    break;
                case Direction.None:
                    PlayerAcceleration = 0f;
                    break;
            }

            UpdateVelocityInTime(deltaTimeMilliseconds);
        }

        private void UpdateVelocityInTime(int deltaTimeMilliseconds)
        {
            // Final Velocity = initial velocity + acceleration * time passed
            float deltaTimeInSeconds = deltaTimeMilliseconds / 1000f;
            float velocityToUse = Velocity + PlayerAcceleration * deltaTimeMilliseconds;
            if (velocityToUse > MaxVelocity)
            {
                velocityToUse = MaxVelocity;
            }
            Velocity = velocityToUse;
        }

        public void NearMiss(float dodgeOffset)
        {
            // Vibrate the gamepad on a specific side of the controller based on where the flame
            // was relative to the player.
        }

        public void RenderPosition(float frameProgress)
        {
            float currentX = Visual.Offset.X;
            float newXValue = TryCheckForLowValue(currentX);
            newXValue = TryCheckForHighValue(currentX);
            // I want to move 4px per second. frame progress uses milliseconds
            newXValue *= frameProgress;
            Visual.Offset += new Vector3(newXValue, Visual.Offset.Y, 0);
        }

        private float TryCheckForLowValue(float currentX)
        {
            float result = currentX + Velocity;
            if (result < 0)
            {
                result = 0;
            }

            return result;

        }

        private float TryCheckForHighValue(float currentX)
        {
            float result = currentX + Velocity;
            // Some kind of max limit
            if (result > 300)
            {
                result = 300;
            }

            return result;
        }
    }
}
