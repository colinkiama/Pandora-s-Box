using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        bool gameLoopEnabled = true;
        int characterSpeed = 17;
        int points = 0;
        public shell()
        {
            this.InitializeComponent();
            characterOffset = 0;


        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Random rnd = new Random();


            while (gameLoopEnabled)
            {
                float randomPosX = rnd.Next(0, (int)Window.Current.Bounds.Width - 20);
                await startCreatingEnemies(randomPosX);
            }

        }

       

        private async Task startCreatingEnemies(float randomPosX)
        {
            await Task.Delay(175);
            //var circleEnemy = new Ellipse();
            //circleEnemy.Fill = new SolidColorBrush(Colors.Red);
            double circleSize = 100;
            var circleEnemy = new flame(200, randomPosX);

            circleEnemy.flameImage.HorizontalAlignment = HorizontalAlignment.Left;
            circleEnemy.flameImage.VerticalAlignment = VerticalAlignment.Top;
            flame.flames.Add(circleEnemy);
            await circleEnemy.flameImage.Offset(randomPosX, -500, 0).StartAsync();
            gameScreen.Children.Add(circleEnemy.flameImage);


            var anim = circleEnemy.flameImage.Offset(randomPosX, (float)(gameScreen.ActualHeight - circleEnemy.flameImage.Height - mainCharacter.ActualHeight + 10), 400, 0, EasingType.Sine);

            anim.Completed += Anim_Completed;
            circleEnemy.isMoving = true;


            await anim.StartAsync();

        }

        private async Task<bool> checkIfHitDetected(Image flame)
        {
            bool wtf = false;

            var flameVisual = flame.TransformToVisual(gameScreen);
            var flamePoint = flameVisual.TransformPoint(new Point(0, 0));
            var flamePointX = flamePoint.X;
            var actualFlameWidth = flame.ActualWidth;

            var mainCharVisual = mainCharacter.TransformToVisual(gameScreen);
            var mainCharPoint = mainCharVisual.TransformPoint(new Point(0, 0));
            var mainCharPointX = mainCharPoint.X;
            var actualMainCharWidth = mainCharacter.ActualWidth;

            await Task.Run(() =>
            {
                for (int i = (int)flamePointX; i < flamePointX + actualFlameWidth + 1; i++)
                {
                    for (int j = (int)mainCharPointX; j < mainCharPointX + actualMainCharWidth; j++)
                    {
                        if (i == j)
                        {
                            Debug.WriteLine("GAME OVER YEAAAAAAAH");
                            wtf = true;
                        }

                    }
                    if (i == flamePointX + actualFlameWidth)
                    {
                        Debug.WriteLine("WTF");
                    }
                }
            });




            return wtf;



        }

        private async void Anim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {

            var anim = (AnimationSet)sender;
            var flame = (Image)anim.Element;




            var thisCircleEnemy = Flames.flame.flames.First();
            bool isHit = await checkIfHitDetected(flame);
            if (!isHit)
            {
                points += 1;
                pointsTextBlock.Text = $"Points: {points}";
                var flameAsVisual = flame.TransformToVisual(gameScreen);
                var flameAsPoint = flameAsVisual.TransformPoint(new Point(0, 0));
                var flameOffSetX = flameAsPoint.X;

                var fadeAnim = flame.Offset((float)flameOffSetX, (float)(gameScreen.ActualHeight - flame.ActualHeight), 100).Fade(0, 100);
                fadeAnim.Completed += FadeAnim_Completed;
                await fadeAnim.StartAsync();
            }
            else
            {
                gameLoopEnabled = false;

            }

        }

        private void FadeAnim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            var anim = (AnimationSet)sender;
            var flame = (Image)anim.Element;


            gameScreen.Children.Remove(flame);

            var thisCircleEnemy = Flames.flame.flames.First();
            Flames.flame.flames.Remove(thisCircleEnemy);
        }

        private async void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Right)
            {
                if (gameLoopEnabled)
                {

                    if (isMovingRight == false)
                    {
                        isMovingRight = true;
                        while (isMovingRight)
                        {
                            if (characterOffset + characterSpeed < gameScreen.ActualWidth - 20)
                            {

                                characterOffset += characterSpeed;
                                await mainCharacter.Offset(characterOffset, duration: 0).StartAsync();
                            }
                            else if (characterOffset < gameScreen.ActualWidth - 20)
                            {
                                await mainCharacter.Offset((float)gameScreen.ActualWidth - 20, duration: 0).StartAsync();
                                isMovingRight = false;
                            }
                            else
                            {
                                isMovingRight = false;
                            }
                        }
                    }
                }
            }

            if (e.Key == Windows.System.VirtualKey.Left)
            {
                if (gameLoopEnabled)
                {

                    if (isMovingLeft == false)
                    {

                        isMovingLeft = true;
                        while (isMovingLeft)
                        {
                            if (characterOffset - characterSpeed > 0)
                            {
                                characterOffset -= characterSpeed;
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
