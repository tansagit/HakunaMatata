using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HakunaMatata.Services
{
    public interface IFileServices
    {
        Dictionary<string, string> UploadFiles(List<IFormFile> files);
        int AddPicture(int realEstateId, List<IFormFile> files);

    }

    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly HakunaMatataContext _context;
        public FileServices(IWebHostEnvironment environment, HakunaMatataContext context)
        {
            _enviroment = environment;
            _context = context;
        }

        public int AddPicture(int realEstateId, List<IFormFile> files)
        {
            var count = 0;
            var uploadedFiles = UploadFiles(files);
            foreach (var file in uploadedFiles)
            {
                var picture = new Picture
                {
                    PictureName = file.Key,
                    RealEstateId = realEstateId,
                    Url = file.Value,
                    IsActive = true
                };
                _context.Picture.Add(picture);

                _context.SaveChanges();
                count++;
            }
            return count;
        }

        public Dictionary<string, string> UploadFiles(List<IFormFile> files)
        {
            string wwwPath = this._enviroment.WebRootPath;
            string contentPath = this._enviroment.ContentRootPath;

            string path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //tao dictionary de luu ten file va duong dan file
            var uploadedFiles = new Dictionary<string, string>();

            foreach (var file in files)
            {
                string fileName = string.Format("{0}-{1}", Guid.NewGuid(), file.FileName);
                var savePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    uploadedFiles.Add(fileName, savePath);
                }
            }
            return uploadedFiles;
        }



    }
}

