using Flames.Interfaces;
using Flames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Flames.Gameplay
{
    public sealed class ViewSpaceHelper
    {
        public static ViewSpaceHelper Instance;
        public List <IPhysicsObject> ViewSpace { get; set; }
        public Panel View;
        
        public static void Create(Panel view)
        {
            Instance = new ViewSpaceHelper(view);
        }

        private ViewSpaceHelper(Panel view)
        {
            View = view;
            var player = new Player();
            AddPlayerToView(player);

            ViewSpace = new List<IPhysicsObject>();
            ViewSpace.Add(player);
        }

        private void AddPlayerToView(Player player)
        {
            ElementCompositionPreview.SetElementChildVisual(View, player.Visual);
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
