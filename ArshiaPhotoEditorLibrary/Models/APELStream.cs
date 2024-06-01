using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArshiaPhotoEditorLibrary.Models
{
    public class APELStream : IDisposable
    {
        private Image _image;
        private string _path;
        public APELStream(string path)
        {
            _image = Image.FromFile(path);
            _path = path;
        }

        public APELStream(Stream stream)
        {
            _image = Image.FromStream(stream);
        }
        public string GetPath() => _path;
        public Image GetImage() => _image;
        public byte[] Save()
        {
            using (MemoryStream storage = new MemoryStream())
            {
                _image.Save(storage, ImageFormat.Jpeg);
                return storage.ToArray();
            }
        }

        public byte[] GetByteArray()
        {
            using (MemoryStream storage = new MemoryStream())
                return storage.ToArray();
        }

        public void Dispose()
        {
            _image.Dispose();
        }
    }
}