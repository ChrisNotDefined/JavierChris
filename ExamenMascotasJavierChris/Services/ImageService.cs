using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExamenMascotasJavierChris.Services
{
    public class ImageService
    {
        public ImageSource ConvertImageFromBase64ToImageSource(string ImageBase64)
        {
            if (!string.IsNullOrEmpty(ImageBase64))
            {
                return ImageSource.FromStream(() =>

                   new MemoryStream(System.Convert.FromBase64String(ImageBase64))
                );
            }
            else
            {
                return null;
            }
        }

        public string ConvertImageSourceToBase64(string filePath)
        {
            if (filePath != null)
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                string file = Convert.ToBase64String(bytes);
                return file;
            }
            else
            {
                return null;
            }
        }

        public string SaveImageFromBase64(string imageBase64, int id)
        {
            if (!string.IsNullOrEmpty(imageBase64))
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), id+"image.tmp");
                byte[] data = Convert.FromBase64String(imageBase64);
                System.IO.File.WriteAllBytes(filePath, data);
                return filePath;
            }
            else
            {
                return "";
            }
        }
    }
}
