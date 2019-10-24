using System;
using System.Drawing;
using System.IO;

namespace Starline
{
    class ClassStarLine
    {
        string ImagemBase64 = null;
        public string GetImageBase64(string PathImage)
        {
            Image img = Image.FromFile(PathImage);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            byte[] ImgEmBytes = ms.ToArray();
            ImagemBase64 = Convert.ToBase64String(ImgEmBytes);
            return ImagemBase64;
        }

        public Image Base64ToImage(string ImagemBase64)
        {
            byte[] imgBytes = Convert.FromBase64String(ImagemBase64);
            MemoryStream ms = new MemoryStream(imgBytes, 0, imgBytes.Length);
            ms.Write(imgBytes, 0, imgBytes.Length);
            Image img = Image.FromStream(ms, true);
            return img;
        }
    }


}
