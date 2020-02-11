using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace AiLibraries.Image
{
    public class ImageHelper
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);


        /// <summary>
        /// แปลง Image เป็น ByteArray
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// แปลง Bitmap เป็น BitmapSource
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static BitmapSource BitmapToBitmapSource(Bitmap imageIn)
        {
            if (imageIn == null) return null;
            var handle = imageIn.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        /// <summary>
        /// แปลงรูปเป็น Base64
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string BitmapToBase64(Bitmap bitmap)
        {
            var bitMap = new Bitmap(bitmap);
            using (var ms2 = new MemoryStream())
            {
                bitMap.Save(ms2, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms2.ToArray());
            }
        }
    }
}