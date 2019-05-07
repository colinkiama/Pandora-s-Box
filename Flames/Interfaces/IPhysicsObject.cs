using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flames.Enum;
using Windows.UI.Composition;

namespace Flames.Interfaces
{
    public interface IPhysicsObject
    {
        SpriteVisual Visual { get; set; }
        float PlayerAcceleration { get; set; }

        void Accelerate(float deltaTimeMilliseconds, Direction movementDirection = Direction.None);
    }
}
