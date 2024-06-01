using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using ArshiaPhotoEditorLibrary.Models;

namespace ArshiaPhotoEditorLibrary.Services
{
    public static class APElSotrage
    {
        private static APELStream _stream;
        public static void SetAPELImageOnStorage(APELStream apelStream)
        {
            _stream = apelStream;
        }

        public static APELImage GetAPELImageOnStorage()
        {
            APELImage apelImage = new APELImage(_stream);
            return apelImage;
        }

        public static byte[] ApelImageToByteArray()
        {
            return _stream.Save();
        }



    }
}