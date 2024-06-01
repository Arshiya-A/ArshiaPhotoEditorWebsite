using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ArshiaPhotoEditorLibrary.Models;

namespace ArshiaPhotoEditorLibrary.Services
{
    class Tools
    {
        APELImage _apelImage;
        Image _image;
        public Tools(APELImage image)
        {
            _apelImage = image;

            if (_apelImage.Path != "")
                _image = new Bitmap(_apelImage.Path);

            else
                _image = new Bitmap(_apelImage.Stream.GetImage());

            ServerImageCompress(_image);

        }

        internal void Brightness(float value)
        {
            using (Graphics g = Graphics.FromImage(_image))
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                float[][] matrix = {
                   new float [] {1,0,0,0,0},
                   new float [] {0,1,0,0,0},
                   new float [] {0,0,1,0,0},
                   new float [] {0,0,0,1,0},
                   new float [] {value,value,value,1,1}
                   };

                ColorMatrix colorMatrix = new ColorMatrix(matrix);
                imageAttributes.SetColorMatrix(colorMatrix);
                g.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, imageAttributes);

            }
            ServerImageCompress(_image);
        }

        internal void Contrast(float value)
        {
            using (Graphics g = Graphics.FromImage(_image))
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                float[][] matrix = {
                   new float [] {value,0,0,0,0},
                   new float [] {0,value,0,0,0},
                   new float [] {0,0,value,0,0},
                   new float [] {0,0,0,1,0},
                   new float [] {0,0,0,1,1}
                   };

                ColorMatrix colorMatrix = new ColorMatrix(matrix);
                imageAttributes.SetColorMatrix(colorMatrix);
                g.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            ServerImageCompress(_image);
        }

        public void CustmeRGB(float r = 1, float g = 1, float b = 1)
        {
            using (Graphics graphics = Graphics.FromImage(_image))
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                float[][] matrix = {
                          new float[] {r,0,0,0,0},
                          new float[] {0,g,0,0,0},
                          new float[] {0,0,b,0,0},
                          new float[] {0,0,0,1,0},
                          new float[] {0,0,0,1,1},
                 };

                ColorMatrix colorMatrix = new ColorMatrix(matrix);
                imageAttributes.SetColorMatrix(colorMatrix);
                graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            ServerImageCompress(_image);
        }

        public void AddWatermark(string text, int positionX, int positionY, int size)
        {
            Point point = new Point();
            point.X = positionX;
            point.Y = positionY;

            Color watermarkColor = Color.White;
            Graphics g = Graphics.FromImage(_image);

            g.DrawString(text, new Font("arial", size, FontStyle.Regular), new SolidBrush(watermarkColor), point);
ServerImageCompress(_image);
        }



        internal byte[] SaveInMemory()
        {
            using (var ms = new MemoryStream())
            {
                // _image.Save(ms, ImageFormat.Png);
                // return ;
                return ServerImageCompress(_image);
            }
        }

        private byte[] ServerImageCompress(Image image)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 1);
            encoderParameters.Param[0] = encoderParameter;

            using (var ms = new MemoryStream())
            {
                _image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecInfos = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo item in codecInfos)
            {
                if (item.FormatID == format.Guid)
                {
                    return item;
                }
            }
            return null;
        }





        // internal void Save(string path, string fileName)
        // {
        //     // string extension = Path.GetExtension(path);
        //     // string file = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        //     // string imageName = file + $"E{extension}";
        //     using (MemoryStream stream = new MemoryStream())
        //     {

        //         stream.Write(SaveInMemory(), 0, SaveInMemory().Length);
        //         var img = Image.FromStream(stream);
        //         img.Save(path + fileName + "." + ".jpeg");
        //     }


        // }

        // internal void Save(byte[] image, string path, string fileName)
        // {
        //     // string extension = Path.GetExtension(path);
        //     // string file = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        //     // string imageName = file + $"E{extension}";
        //     // using (MemoryStream stream = new MemoryStream())
        //     // {

        //     //     stream.Write(image, 0, image.Length);
        //     //     var img = Image.FromStream(stream);
        //     //     img.Save(path + fileName + "." + ".jpeg");
        //     // }


        // }


    }
}