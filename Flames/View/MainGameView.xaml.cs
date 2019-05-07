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
        public MainGameView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StartGame();
            HIDHelper.Instance.ButtonPressedDown += Instance_ButtonPressedDown;
            HIDHelper.Instance.ButtonReleased += Instance_ButtonReleased;

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
            
        }
    }
}
