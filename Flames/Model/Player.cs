using Flames.Enum;
using Flames.Gameplay;
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
        const float AccelerationPerSecond = 4f;


        public Player(Compositor compositor)
        {
            Visual = compositor.CreateSpriteVisual();
            Visual.Brush = compositor.CreateColorBrush(Colors.Purple);
            Visual.Size = new Vector2(40, 40);
            HIDHelper.Instance.LeftDown += Instance_LeftDown;
            HIDHelper.Instance.RightDown += Instance_RightDown;
        }

        private void Instance_RightDown(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            float currentX = Visual.Offset.X + AccelerationPerSecond;
            currentX = TryCheckForLowValue(currentX);
            currentX = TryCheckForHighValue(currentX);

            
            Visual.Offset = new Vector3(currentX, 0, 0);
        }

        private void Instance_LeftDown(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            float currentX = Visual.Offset.X - AccelerationPerSecond;
            currentX = TryCheckForLowValue(currentX);
            currentX = TryCheckForHighValue(currentX);

            Visual.Offset = new Vector3(currentX, 0, 0);
        }

        public void Accelerate(Direction movementDirection = Direction.None)
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

            UpdateVelocityInTime();
        }

        private void UpdateVelocityInTime()
        {
            // Final Velocity = initial velocity + acceleration * time passed
            float velocityToUse = Velocity + PlayerAcceleration;
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

        public void RenderPosition()
        {

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
