using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArshiaPhotoEditorWebsite.Models;
using System.ComponentModel.DataAnnotations.Schema;
using ArshiaPhotoEditorLibrary.Interfaces;
using ArshiaPhotoEditorLibrary.Services;
using ArshiaPhotoEditorLibrary.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shyjus.BrowserDetection;
using Microsoft.AspNetCore.Server.HttpSys;

namespace ArshiaPhotoEditorWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
    private Timer _timer;
    private IAPEL _apel;
    private int _executionCount = 0;



    public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IBrowserDetector browserDetector)
    {
        _logger = logger;
        _environment = environment;
        _apel = new APEL();
    }

    public IActionResult Index()
    {
        Photo photo = new Photo();
        return View(photo);
    }



    [HttpPost]
    public IActionResult UploadImage(Photo photo)
    {
        if (photo.FormFile != null)
        {
            string extension = Path.GetExtension(photo.FormFile.FileName);
            if (extension != ".jpeg" && extension != ".png" && extension != ".jpg")
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorName = "Format Error",
                    ErrorDescription = "This file is not image or you Imageformat not supported . Folowing Formats Supported: png-jpg-jpeg",
                    Problem = $"Uploaded File : {photo.FormFile.FileName}"
                });
            }
            else
            {

                // string wwwrootpath = _environment.WebRootPath;
                // string folderName = "ImageCaches";
                // string fileName = Guid.NewGuid().ToString().Replace("-", " ") + photo.FormFile.FileName;
                // string fullPath = Path.Combine(wwwrootpath, folderName, fileName);


                // if (!Directory.Exists(Path.Combine(wwwrootpath, folderName)))
                // {
                //     Directory.CreateDirectory(Path.Combine(wwwrootpath, folderName));
                // }
               

                // using (FileStream fs = System.IO.File.Create(Path.Combine(wwwrootpath, folderName, fileName)))
                // {
                //     photo.FormFile.CopyTo(fs);
                //     fs.Dispose();
                //     fs.Close();
                // }


                // using (FileStream fileStream = System.IO.File.Open(Path.Combine(wwwrootpath, folderName, fileName), FileMode.Open))
                // {
                //     fileStream.Close();
                //     fileStream.Dispose();
                // }
                APELStream stream = new APELStream(photo.FormFile.OpenReadStream());
                APElSotrage.SetAPELImageOnStorage(stream);
                _apel.Ready(APElSotrage.GetAPELImageOnStorage());

                var image = new Photo()
                {
                    FormFile = photo.FormFile,
                    // ImageName = fileName,
                    ImageLive = APElSotrage.ApelImageToByteArray(),
                    IsEdit = false
                };

                string imreBase64Data = Convert.ToBase64String(image.ImageLive);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                ViewBag.ImageByte = imgDataURL;


                // photo.FormFile.OpenReadStream().Dispose();


                return View(nameof(Index), image);
            }


        }

        else
        {
            return View("Error", new ErrorViewModel() { ErrorName = "Upload Error", ErrorDescription = "You didn't upload image" });
        }

    }

    public IActionResult ImageViewer()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> ApplyChanges(Photo image)
    {

        // string imagePath = Path.Combine(_environment.WebRootPath, "ImageCaches", image.ImageName);


        _apel.Ready(APElSotrage.GetAPELImageOnStorage());

        _apel.Brightness(image.EditorParameters.Brightness);
        _apel.Contrast(image.EditorParameters.Contrast);
        _apel.CustomeRGB(image.EditorParameters.Rcolor, image.EditorParameters.Gcolor, image.EditorParameters.Bcolor);
        _apel.AddWaterMark(image.EditorParameters.WatermarkText, image.EditorParameters.PositionX, image.EditorParameters.PositionY,
        image.EditorParameters.Size);


        var photo = new Photo()
        {
            ImageLive = _apel.SaveInMemory(),
            ImageName = image.ImageName,
            EditorParameters = image.EditorParameters,
            IsEdit = true,
        };


        string imreBase64Data = Convert.ToBase64String(photo.ImageLive);
        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
        ViewBag.ImageByte = imgDataURL;



        return await Task.Run(async () =>
        {
            return View("Index", photo);
        });

    }

    public IActionResult SaveImage(Photo photo, string exportImageName)
    {
        string imagePath = Path.Combine(_environment.WebRootPath, "ImageCaches", photo.ImageName);

        _apel.Ready(APElSotrage.GetAPELImageOnStorage());



        // apelImage.Dispose();

        // if (System.IO.File.Exists(imagePath))
        // {
        //     System.GC.Collect();
        //     System.GC.WaitForPendingFinalizers();
        //     System.IO.File.Delete(imagePath);
        // }
        if (exportImageName == null)
            exportImageName = "ArshiaPhotoEditor";


        return File(photo.ImageLive, "application/octet-stream", $"{exportImageName}.jpeg");

    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int id)
    {
        int errorType = id;
        if (errorType == 404 || errorType == 405)
            return View(new ErrorViewModel { ErrorName = "Page NotFound", ErrorDescription = "Page that you searched ,Is not found" });

        if (errorType == 8080)
            return View(new ErrorViewModel { ErrorName = "Page NotFound", ErrorDescription = "Page that you searched ,Is not found" });



        else
            errorType = 1010;

        if (errorType == 1010)
            return View(new ErrorViewModel { ErrorName = "Page NotFound", ErrorDescription = "Page that you searched ,Is not found" });


        return View("Error/1010");

    }


}
