using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Utils
{
    public static class BitmapImageHelper
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bitmapImage = null;
            try
            {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(byteArray);
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            catch
            {
                bitmapImage = null;
            }
            return bitmapImage;
        }

        public static byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] byteArray = null;
            try
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byteArray = ms.ToArray();
                }
            }
            catch (Exception e)
            {
                //other exception handling 
                Console.WriteLine(e);
            }
            return byteArray;
        }
    }
}
