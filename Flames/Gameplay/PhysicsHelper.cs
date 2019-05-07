﻿using Flames.Enum;
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
        internal List<IPhysicsObject> InGameObjects;
        float timePassed = 0;

        public PhysicsHelper(List<IPhysicsObject> ViewSpace)
        {
            InGameObjects = ViewSpace;
        }

        public void UpdatePosition(Direction movementDirection, float deltaTimeMilliseconds)
        {
            for (int i = 0; i < InGameObjects.Count; i++)
            {
                InGameObjects[i].Accelerate(deltaTimeMilliseconds, movementDirection);
            }
        }

    }
}
