using Flames.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Flames.Gameplay
{
    public sealed class HIDHelper
    {
        private static Lazy<HIDHelper> lazy =
            new Lazy<HIDHelper>(() => new HIDHelper());

        public static HIDHelper Instance = lazy.Value;
        public event EventHandler<KeyEventArgs> LeftDown;
        public event EventHandler<KeyEventArgs> RightDown;


        private readonly object myLock = new object();
        private List<Gamepad> myGamepads = new List<Gamepad>();
        private Gamepad mainGamepad;

        public HIDHelper()
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            //Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            
            switch (args.VirtualKey)
            {

                case VirtualKey.Left:
                    LeftDown?.Invoke(this, args);
                    break;
                case VirtualKey.GamepadDPadLeft:
                    LeftDown?.Invoke(this, args);
                    break;
                case VirtualKey.GamepadLeftThumbstickLeft:
                    LeftDown?.Invoke(this, args);
                    break;
                case VirtualKey.Right:
                    RightDown?.Invoke(this, args);
                    break;
                case VirtualKey.GamepadDPadRight:
                    RightDown?.Invoke(this, args);
                    break;
                case VirtualKey.GamepadLeftThumbstickRight:
                    RightDown?.Invoke(this, args);
                    break;
            }

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

        internal Direction PollInputs()
        {
            Direction currentDirection = Direction.None;

            if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down))
            {
                currentDirection = Direction.Left;
            }
            else if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down))
            {
                currentDirection = Direction.Right;
            }
            return currentDirection;
        }
    }
}
