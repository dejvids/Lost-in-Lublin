using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using  System.Drawing;

namespace LostInLublin.Services
{
    public class ImageLoaderService
    {
        private Bitmap image;
        public ImageLoaderService()
        {

        }

        public Bitmap GetBytes(string encodedString)
        {
            var bitmapData = Convert.FromBase64String(FixBase64ForImage(encodedString));
            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
            Bitmap bitImage = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
            image = bitImage;
            return bitImage;
        }
        public async Task<string> SaveFile()
        {
            //  FileStream fs = new FileStream("/imgs/",FileMode.CreateNew,FileAccess.Write,FileShare.Write,sizeof(byte) * file.Length,true);
            string filename = Path.GetRandomFileName() + ".png";
            var targetFilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "uploaded",  filename);

            try
            {
                image.Save(targetFilePath);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty ;
            }
            return $"https://zgubionewlublinie.azurewebsites.net/uploaded/{filename}";
        }

        public string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }
    }
}
