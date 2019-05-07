using Flames.Enum;
using Flames.Interfaces;
using System;
using System.Collections.Generic;
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
        private Compositor _compositor;
        public SpriteVisual Visual { get; set; }
        public float PlayerAcceleration { get; set; }
        public float Velocity { get; set; }
        const float MaxVelocity = 12.0f;
        const float AccelerationPerSecond = -4f;


        public Player()
        {
            Visual = _compositor.CreateSpriteVisual();
            Visual.Brush = _compositor.CreateColorBrush(Colors.Purple);
            Visual.Size = new Vector2(40, 40);
        }

        public void Accelerate(float deltaTimeMilliseconds, Direction movementDirection = Direction.None)
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

        private void UpdateVelocityInTime(float deltaTimeMilliseconds)
        {
            // Final Velocity = initial velocity + acceleration * time passed
            float deltaTimeInSeconds = deltaTimeMilliseconds / 1000f;
            float velocityToUse = Velocity + PlayerAcceleration * deltaTimeMilliseconds;
            if (velocityToUse > MaxVelocity)
            {
                velocityToUse = MaxVelocity;
            }
        }

        public void NearMiss(float dodgeOffset)
        {
            // Vibrate the gamepad on a specific side of the controller based on where the flame
            // was relative to the player.
        }

        public void RenderPosition()
        {
            Visual.Offset += new Vector3(Velocity, Visual.Offset.Y, 0);
        }
    }
}
