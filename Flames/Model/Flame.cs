using Flames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace Flames.Model
{
    public class Flame : IPhysicsObject
    {
        const float Gravity = 1f;
        public SpriteVisual Visual { get; set; }
        public float PlayerAcceleration { get; set; }
        public float Velocity { get; set; }
        public float MaxSpeed { get; set; }
    }
}
