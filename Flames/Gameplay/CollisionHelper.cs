using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flames.Interfaces;

namespace Flames.Gameplay
{
    class CollisionHelper
    {
        private List<IPhysicsObject> viewSpace;

        public CollisionHelper(List<IPhysicsObject> viewSpace)
        {
            this.viewSpace = viewSpace;
        }

        internal void DetectCollision()
        {
            throw new NotImplementedException();
        }
    }
}
