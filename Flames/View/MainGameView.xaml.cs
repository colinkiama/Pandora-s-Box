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
        int msPerUpdate = 16;
        // Environment.TickCount updates at approximately 16ms;
        int _totalMillisecondsPassed = 0;
        int frames = 0;
        int millisecondsInASecond = 1000;
        int _deltaTimeMilliseconds = 0;
        PhysicsHelper _physicsHelper;
        CollisionHelper _collisionHelper;

        public MainGameView()
        {
            this.InitializeComponent();
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

            int previous = GetCurrentTime();
            int lag = 0;
            while (willQuit == false)
            {
                int elapsed = 0;

                frames++;
                int current = GetCurrentTime();
                elapsed = current - previous;
                previous = current;
                lag += elapsed;
                _deltaTimeMilliseconds += elapsed;


                Direction movementDirection = HIDHelper.Instance.PollInputs();


                while (lag >= msPerUpdate)
                {
                    _physicsHelper.UpdatePosition(movementDirection, elapsed);
                    lag -= msPerUpdate;
                }

                _collisionHelper.DetectCollision();

                await Task.Run(() =>  ViewSpaceHelper.Instance.RenderPositions(lag / msPerUpdate));






                UpdateFPS();
                //GameLoop(_deltaTimeMilliseconds);



            }

        }

        private int GetCurrentTime()
        {
            return Environment.TickCount;
        }

        private void WriteMilliseconds(int endMilliseconds, int beginMilliseconds)
        {
            Debug.WriteLine($"Milliseconds in frame: {endMilliseconds - beginMilliseconds}");
        }

        private void UpdateFPS()
        {
            _totalMillisecondsPassed += _deltaTimeMilliseconds;
            Debug.WriteLine($"Total milliseconds passed: {_totalMillisecondsPassed}");
            double seconds = _totalMillisecondsPassed / millisecondsInASecond;
            Debug.WriteLine($"Seconds: {seconds}");
            Debug.WriteLine($"Frame: {frames}");
            Debug.WriteLine($"FPS: {frames / seconds}");
        }

        private void GameLoop(float deltaTimeMilliseconds)
        {

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            willQuit = true;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            await StartGame();
            //StartGame();
        }
    }
}
