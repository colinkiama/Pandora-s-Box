using Flames.Enum;
using Flames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flames.Gameplay
{
    public class PhysicsHelper
    {
        internal List<IPhysicsObject> InGameObjects = new List<IPhysicsObject>();
        float timePassed = 0;
        public void UpdatePosition(Direction movementDirection , float deltaTimeMilliseconds)
        {
            
            for (int i = 0; i < InGameObjects.Count; i++)
            {
                if (i == 0)
                {
                    switch (movementDirection)
                    {
                        case (Direction.Left | Direction.Right):
                            InGameObjects[i].Accelerate(movementDirection);
                            break;

                        case Direction.None:
                            InGameObjects[i].Decelerate();
                            break;

                    }
                }
                else
                {
                    InGameObjects[i].Accelerate();
                }
                
                
            }
        }

    }
}
