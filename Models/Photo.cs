using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArshiaPhotoEditorWebsite.Models
{
    public class Photo
    {
        // public int ImageID { get; set; }
        public IFormFile FormFile { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageLive { get; set; }
        public string BeforeImageName { get; set; }
        public EditorParameters EditorParameters { get; set; }
        public bool IsEdit { get; set; }
    }
}