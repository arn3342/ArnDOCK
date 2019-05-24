using System;
using System.Windows.Media.Imaging;

namespace dock
{
    public class ImageLoader
    {
        public BitmapImage LoadImage(Uri uri, int Pixel)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = uri;
            bi.DecodePixelWidth = Pixel;
            bi.EndInit();
            bi.Freeze();
            return bi;
        }
    }
}
