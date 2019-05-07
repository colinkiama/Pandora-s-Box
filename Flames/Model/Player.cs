using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace Flames.Model
{
    public class Player
    {
        public SpriteVisual PlayerVisual { get; set; }

        public void NearMiss(float dodgeOffset)
        {
            // Vibtrate the controller on a specific side of the controller based on where the flame
            // was relative to the player.
        }
    }
}
