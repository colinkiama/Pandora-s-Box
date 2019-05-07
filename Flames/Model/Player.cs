using Flames.Enum;
using Flames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace Flames.Model
{
    public class Player: IPhysicsObject
    {
        public SpriteVisual Visual { get; set; }
        public float PlayerAcceleration { get; set; }
        public float Velocity { get; set; }
        public float MaxSpeed { get; set; }
        

        public void Accelerate(Direction movementDirection = Direction.None)
        {
            switch (movementDirection)
            {
                case Direction.Left:

                    PlayerAcceleration = PlayerAcceleration > 0 ? 4 : 3;
                    break;
                case Direction.Right:
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
        }

        public void NearMiss(float dodgeOffset)
        {
            // Vibtrate the controller on a specific side of the controller based on where the flame
            // was relative to the player.
        }


    }
}
