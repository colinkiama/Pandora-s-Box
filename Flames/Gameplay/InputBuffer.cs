using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Flames.Gameplay
{
    internal static class InputBuffer
    {
        public static Queue<VirtualKey> buffer { get; set; }
    }
}
