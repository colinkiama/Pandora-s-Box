using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Flames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class shell : Page
    {
        int characterOffset = 0;
        bool isMovingRight = false;
        bool isMovingLeft = false;
        bool isDead = false;
        public shell()
        {
            this.InitializeComponent();
            characterOffset = 0;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            startCreatingEnemies();
        }

        private async void startCreatingEnemies()
        {
            var circleEnemy = new Ellipse();
            circleEnemy.Fill = new SolidColorBrush(Colors.Red);
            circleEnemy.Width = 20;
            circleEnemy.Height = 20;
            circleEnemy.HorizontalAlignment = HorizontalAlignment.Left;
            circleEnemy.VerticalAlignment = VerticalAlignment.Top;
            gameScreen.Children.Add(circleEnemy);
            do
            {
                await circleEnemy.Offset(100).StartAsync();
                await circleEnemy.Offset(0).StartAsync();
            } while (isDead == false);


        }

        private async void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Right)
            {
                if (isMovingRight == false)
                {
                    isMovingRight = true;
                    while (isMovingRight)
                    {
                        if (characterOffset + 10 < gameScreen.ActualWidth - 20)
                        {

                        characterOffset += 10;
                        await mainCharacter.Offset(characterOffset, duration: 0).StartAsync();
                        }
                        else if (characterOffset < gameScreen.ActualWidth - 20)
                        {
                            await mainCharacter.Offset((float)gameScreen.ActualWidth -20, duration: 0).StartAsync();
                            isMovingRight = false;
                        }
                        else
                        {
                            isMovingRight = false;
                        }
                    }
                }
            }

            if (e.Key == Windows.System.VirtualKey.Left)
            {
                if (isMovingLeft == false)
                {

                    isMovingLeft = true;
                    while (isMovingLeft)
                    {
                        if (characterOffset - 10 > 0)
                        {
                            characterOffset -= 10;
                            await mainCharacter.Offset(characterOffset, duration: 0).StartAsync();
                            
                        }
                        else if (characterOffset > 0)
                        {
                            await mainCharacter.Offset(0, duration: 0).StartAsync();
                            isMovingLeft = false;
                        }

                        else
                        {
                            isMovingLeft = false;
                        }
                    }
                }
            }
            
        }

        private async void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Right)
            {
                if (isMovingRight == true)
                {
                    isMovingRight = false;
                }
            }

            if (e.Key == Windows.System.VirtualKey.Left)
            {
                if (isMovingLeft == true)
                {
                    isMovingLeft = false;
                }
            }
        }
    }
}
