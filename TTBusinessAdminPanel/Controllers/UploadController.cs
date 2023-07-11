using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.IO;
using System;
using ApplicationService.IServices;
using AspNetCoreHero.ToastNotification.Abstractions;
using NLog;
using CommonService.Constants;
using CommonService.Enums;

namespace TTBusinessAdminPanel.Controllers
{
    public class UploadController : Controller
    {
        private Logger _logger;
        public UploadController()
        {
            _logger = LogManager.GetLogger("Upload");
        }
        [HttpPost]
        public IActionResult Upload()
        {
            var imageList = new ArrayList();
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                   // string filePath = GetFileUploadDetails(uploadtype);
                    string tempPath =CommonConstants.FileTempPath;
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {

                        var postedFile = Request.Form.Files[i];
                        if (postedFile != null)
                        {
                            string newName = string.Empty;
                            newName = DateTime.Now.ToString("ddMMyyyyhhmmss")+"_"+postedFile.FileName;
                            string newPath=Path.Combine(tempPath,newName);
                            using (var stream = new FileStream(newPath, FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                imageList.Add(newPath);
                                stream.Flush();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Info("UploadController--> Upload: An error occured ");
                _logger.Error(ex);
            }
            return Json(imageList);
        }
   
    }
}
