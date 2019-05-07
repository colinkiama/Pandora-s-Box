using Flames.Enum;
using Flames.Gameplay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Flames.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainGameView : Page
    {
        const float milliSecondsInSeconds = 1000;
        
        // Environment.TickCount updates at approximately 16ms;
        const float highResolutionTimerFrequency = 16.0f;

        float _deltaTimeMilliseconds;
        PhysicsHelper _physicsHelper = new PhysicsHelper();
        CollisionHelper _collisionHelper = new CollisionHelper();
        ViewSpaceHelper _viewSpaceHelper = new ViewSpaceHelper();
        public MainGameView()
        {
            this.InitializeComponent();
            // Targeting 60FPS at first
            _deltaTimeMilliseconds = milliSecondsInSeconds / 60.0f;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StartGame();

        }

        private void Instance_ButtonReleased(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Down)
            {
                Debug.WriteLine("Pressed");
            }
        }

        private void Instance_ButtonPressedDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Down)
            {
                Debug.WriteLine("HOLD IT DOWN!");
            }
        }

        private void StartGame()
        {
            int beginMilliseconds = Environment.TickCount;
            while (true)
            {
                GameLoop(_deltaTimeMilliseconds);
                int endMilliseconds = Environment.TickCount;
                _deltaTimeMilliseconds = (float)(endMilliseconds - beginMilliseconds) /
                    highResolutionTimerFrequency;
                beginMilliseconds = endMilliseconds;
            }
        }

        private void GameLoop(float deltaTimeMilliseconds)
        {
            Direction movementDirection = HIDHelper.Instance.PollInputs();
            _physicsHelper.UpdatePosition(movementDirection, deltaTimeMilliseconds);
            _collisionHelper.DetectCollision();
            _viewSpaceHelper.UpdateScene();
        }
    }
}
