using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace RandomPicture
{
    class RefreshPicture
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(
           int uAction, int uParam, string lpvParam, int fuWinIni);

        // Константы
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public enum WallpaperStyle : int
        {
            Tiled, Centered, Stretched
        }
        public enum SPI : uint
        {
            SPI_GETSCREENSAVEACTIVE = 0x0010,
            SPI_SETSCREENSAVEACTIVE = 0x0011
        }
        public enum SPIF : uint
        {
            None = 0x00,
            SPIF_UPDATEINIFILE = 0x01,
            SPIF_SENDCHANGE = 0x02,
            SPIF_SENDWININICHANGE = 0x02
        }
        public static void AddPicture(ListBox listBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPG(*.JPG)|*.JPG|PNG(*.PNG)|*.PNG|GIF(*.GIF)|*.GIF|GPE(*GPE)|.GPE|BPM(*.BMP)|*.BMP";
            openFileDialog.ShowDialog();
            foreach(string item in openFileDialog.FileNames)
            {
                listBox.Items.Add(item);
            }
        }
        public static void RefreshPicturer(ListBox listBox, int hour, int min)
        {
            if(listBox.Items != null) {
                
                
                Random random = new Random();

                List<string> picture = new List<string>();
                foreach (string file in listBox.Items)
                {
                    if (IsImage.BoolIsImage(file))
                    {
                        picture.Add(file);
                    }
                }
                var pictur = picture.OrderBy(Index => random.Next());
                Queue<string> queue = new Queue<string>(pictur);
                while (queue.Count != 0)
                {
                    string imageFileName;
                    imageFileName = queue.Dequeue().ToString();

                    RegistryKey key = Registry.CurrentUser.OpenSubKey(
                        "Control Panel\\Desktop", true);

                    //WallpaperStyle style;

                    //style = (WallpaperStyle)Enum.Parse(typeof(WallpaperStyle),
                    //    "Tiled");

                    //switch (style)
                    //{
                    //    case WallpaperStyle.Stretched:
                    //        key.SetValue(@"WallpaperStyle", "2");
                    //        key.SetValue(@"TileWallpaper", "0");
                    //        break;

                    //    case WallpaperStyle.Centered:
                    //        key.SetValue(@"WallpaperStyle", "1");
                    //        key.SetValue(@"TileWallpaper", "0");
                    //        break;

                    //    case WallpaperStyle.Tiled:
                    key.SetValue(@"WallpaperStyle", "1");
                    key.SetValue(@"TileWallpaper", "1");
                    //        break;
                    //}

                    SystemParametersInfo(SPI_SETDESKWALLPAPER, 0,
                        imageFileName, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
                        
                    Thread.Sleep(3600000*hour+ 60000*min);
                }
                //Thread.Sleep(10000);
                
            }
        }
    }
}
