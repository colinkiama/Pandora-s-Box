using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Flames
{
   public sealed class flame
    {
        BitmapImage flameImageSource = new BitmapImage(new Uri("ms-appx:///Assets/flame.png"));
        public Image flameImage { get; set; }
        public bool isPutout { get; set; }
        public bool isMoving { get; set; }
        public float animXOffset { get; set; }

        public flame(double imageSize, float xOffset)
        {
            flameImage = new Image();
            flameImage.Source = flameImageSource;
            flameImage.Width = imageSize;
            flameImage.Height = imageSize * 2;
            flameImage.Opacity = 1;
           
            isMoving = false;
        }

        public static ObservableCollection<flame> flames = new ObservableCollection<flame>();
    }
}
