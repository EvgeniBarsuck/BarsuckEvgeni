using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RandomPicture
{
    class IsImage
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public static bool BoolIsImage(string path)
        {
            return ImageExtensions.Contains(Path.GetExtension(path).ToUpperInvariant());
        }
    }
}
