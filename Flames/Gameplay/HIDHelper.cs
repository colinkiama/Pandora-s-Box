using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Flames.Gameplay
{
    public sealed class HIDHelper
    {
        private static Lazy<HIDHelper> lazy =
            new Lazy<HIDHelper>(() => new HIDHelper());

        public static HIDHelper Instance = lazy.Value;

        private readonly object myLock = new object();
        private List<Gamepad> myGamepads = new List<Gamepad>();
        private Gamepad mainGamepad;

        public HIDHelper()
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
        }

        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            lock (myLock)
            {
                int indexRemoved = myGamepads.IndexOf(e);

                if (indexRemoved > -1)
                {
                    if (mainGamepad == myGamepads[indexRemoved])
                    {
                        mainGamepad = null;
                    }

                    myGamepads.RemoveAt(indexRemoved);
                }
            }
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            lock (myLock)
            {
                bool gamepadInList = myGamepads.Contains(e);

                if (!gamepadInList)
                {
                    myGamepads.Add(e);
                }
            }
        }

        

        
        private void GetGamepads()
        {
            lock (myLock)
            {
                foreach (var gamepad in Gamepad.Gamepads)
                {
                    // Check if the gamepad is already in myGamepads; if it isn't, add it.
                    bool gamepadInList = myGamepads.Contains(gamepad);

                    if (!gamepadInList)
                    {
                        // This code assumes that you're interested in all gamepads.
                        myGamepads.Add(gamepad);
                    }
                }
            }
        }

        internal void PollInputs()
        {
            throw new NotImplementedException();
        }
    }
}
