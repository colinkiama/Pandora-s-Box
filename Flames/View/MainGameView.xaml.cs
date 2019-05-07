using Flames.Enum;
using Flames.Gameplay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        bool willQuit = false;
        // Environment.TickCount updates at approximately 16ms;
        const float highResolutionTimerFrequency = 16.0f;

        float _deltaTimeMilliseconds;
        PhysicsHelper _physicsHelper;
        CollisionHelper _collisionHelper;

        public MainGameView()
        {
            this.InitializeComponent();

            // Targeting 60FPS at first
            _deltaTimeMilliseconds = milliSecondsInSeconds / 60.0f;
            ViewSpaceHelper.Create(rootStackPanel);
            _physicsHelper = new PhysicsHelper(ViewSpaceHelper.Instance.ViewSpace);
            _collisionHelper = new CollisionHelper(ViewSpaceHelper.Instance.ViewSpace);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }



        private async Task StartGame()
        {
            int beginMilliseconds = Environment.TickCount;
            while (willQuit == false)
            {
                await GameLoop(_deltaTimeMilliseconds);
                int endMilliseconds = Environment.TickCount;
                _deltaTimeMilliseconds = (float)(endMilliseconds - beginMilliseconds) /
                    highResolutionTimerFrequency;
                beginMilliseconds = endMilliseconds;
            }

        }

        private async Task GameLoop(float deltaTimeMilliseconds)
        {
            Direction movementDirection = HIDHelper.Instance.PollInputs();
            await Task.Run(() =>
            {

                _physicsHelper.UpdatePosition(movementDirection, deltaTimeMilliseconds);
                _collisionHelper.DetectCollision();
                ViewSpaceHelper.Instance.RenderPositions();
            });
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            willQuit = true;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            await StartGame();
        }
    }
}
