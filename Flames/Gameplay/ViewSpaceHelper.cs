using Flames.Interfaces;
using Flames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
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
            ViewSpace = new List<IPhysicsObject>();

            view.Loaded += (s, e) =>
            {
                var panel = (Panel)s;
                var panelVisual = ElementCompositionPreview.GetElementVisual(panel);

                var player = new Player(panelVisual.Compositor);
                ElementCompositionPreview.SetElementChildVisual(panel, player.Visual);
                
                ViewSpace.Add(player);
            };
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
