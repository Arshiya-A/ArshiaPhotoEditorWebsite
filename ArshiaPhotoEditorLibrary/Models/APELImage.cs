using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace ArshiaPhotoEditorLibrary.Models
{
    public class APELImage : IDisposable
    {
        private Image _image;
        private APELStream _apelStream;
        private string _imagePath = "";

        public APELImage(string imagePath)
        {
            _imagePath = imagePath;
            _image = Image.FromFile(_imagePath);
        }

        public APELImage(APELStream apelStream)
        {
            _apelStream = apelStream;
            _image = _apelStream.GetImage();
        }


        public int Width
        {
            get
            {
                return _image.Width;
            }
            set
            {
                Width = value;
            }
        }
        public int Height
        {
            get
            {
                return _image.Height;
            }
            set
            {
                Height = value;
            }
        }
        public string Path
        {
            get
            {
                return _imagePath;
            }
            set
            {
                Path = value;
            }
        }

        public APELStream Stream
        {
            get
            {
                return _apelStream;
            }
            set
            {
                Stream = value;
            }
        }





        public void Dispose()
        {

            _image.Dispose();

        }
    }




    // public void SetFilter(int R = 0, int G = 0, int B = 0)
    // {
    //     for (int w = 0; w < Width; w++)
    //     {
    //         for (int h = 0; h < Height; h++)
    //         {
    //             var colorValue = _image.GetPixel(w, h);
    //             var avaregeValue = colorValue.R / 3 + colorValue.G / 3 + colorValue.B / 3;
    //             _image.SetPixel(w, h, Color.FromArgb(avaregeValue, R, G, B));
    //         }
    //     }
    // }

    // public void Brightness(float value)
    // {


    // }

    // public void Save(string path)
    // {
    //     _image.Save(path);
    // }







}
