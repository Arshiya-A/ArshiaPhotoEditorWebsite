using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArshiaPhotoEditorWebsite.Views.Shared
{
    public class ImageViewer : PageModel
    {
        private readonly ILogger<ImageViewer> _logger;

        public ImageViewer(ILogger<ImageViewer> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}