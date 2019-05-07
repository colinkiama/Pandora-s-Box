using Flames.Interfaces;
using Flames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flames.Gameplay
{
    public sealed class ViewSpaceHelper
    {
        public static ViewSpaceHelper Instance;
        public List <IPhysicsObject> ViewSpace { get; set; }
        
        public static void Create()
        {
            Instance = new ViewSpaceHelper();
        }

        private ViewSpaceHelper()
        {
            ViewSpace = new List<IPhysicsObject>();
            ViewSpace.Add(new Player());
        }

        public void RenderPositions()
        {
            for (int i = 0; i < ViewSpace.Count; i++)
            {
                ViewSpace[i].RenderPosition();
            }
        }
    }
}
