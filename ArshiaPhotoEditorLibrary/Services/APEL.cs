using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ArshiaPhotoEditorLibrary.Interfaces;
using ArshiaPhotoEditorLibrary.Models;

namespace ArshiaPhotoEditorLibrary.Services
{
    public class APEL : IAPEL
    {
        private APELImage _apelImage;
        private Tools _tools;
        public void Ready(APELImage image)
        {
            _apelImage = image;
            _tools = new Tools(_apelImage);
        }
        


        public void Brightness(float value) => _tools.Brightness(value);
        public void Contrast(float value) => _tools.Contrast(value);
        public void CustomeRGB(float r, float g, float b) => _tools.CustmeRGB(r, g, b);
        public void AddWaterMark(string text, int positionX, int positionY, int size) => _tools.AddWatermark(text, positionX, positionY, size);
        // public void Save(string path, string fileName) => _tools.Save(path, fileName);
        public byte[] SaveInMemory() => _tools.SaveInMemory();

        
        
    }
}