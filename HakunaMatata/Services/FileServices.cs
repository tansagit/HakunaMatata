using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Services
{
    public interface IFileServices
    {
        Dictionary<string, string> UploadFiles(List<IFormFile> files);
        int AddPicture(int realEstateId, List<IFormFile> files);
        IEnumerable<Picture> GetPicturesForRealEstate(int id);
        bool RemovePictureFromRealEstate(int pictureId);
        int? GetRealEstateId(int pictureId);
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


        public IEnumerable<Picture> GetPicturesForRealEstate(int id)
        {
            return _context.Picture
                .Where(p => p.RealEstateId == id && p.IsActive == true).ToList();
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

        public bool RemovePictureFromRealEstate(int pictureId)
        {
            var picture = _context.Picture.Find(pictureId);
            if (picture != null)
            {
                _context.Picture.Remove(picture);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Dictionary<string, string> UploadFiles(List<IFormFile> files)
        {
            string wwwPath = this._enviroment.WebRootPath;
            //string contentPath = this._enviroment.ContentRootPath;

            string path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //tao dictionary de luu ten file va duong dan file
            var uploadedFiles = new Dictionary<string, string>();

            foreach (var file in files)
            {
                string fileUrl = string.Format("{0}-{1}", Guid.NewGuid(), file.FileName);

                //dung de luu trong database de nhan biet la file local
                //fileURL : GUID + FileName
                //fileName: local-picture + fileURL
                string fileName = string.Format("local-picture-{0}", fileUrl);

                var savePath = Path.Combine(path, fileUrl);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    uploadedFiles.Add(fileName, fileUrl);
                }
            }
            return uploadedFiles;
        }

        public int? GetRealEstateId(int pictureId)
        {
            var picture = _context.Picture.Find(pictureId);
            if (picture != null)
            {
                return picture.RealEstateId;
            }
            return null;
        }
    }
}

