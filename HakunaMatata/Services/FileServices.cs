using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public interface IFileServices
{
    Task<Dictionary<string, string>> UploadFiles(List<IFormFile> files);
    Task<int> AddPicture(int realEstateId, int userId, List<IFormFile> files);

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

    public async Task<int> AddPicture(int realEstateId, int userId, List<IFormFile> files)
    {
        var count = 0;
        var uploadedFiles = await this.UploadFiles(files);
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

            await _context.SaveChangesAsync();
            var id = picture.Id;
            count++;
        }
        return count;
    }

    public async Task<Dictionary<string, string>> UploadFiles(List<IFormFile> files)
    {
        string wwwPath = this._enviroment.WebRootPath;
        string contentPath = this._enviroment.ContentRootPath;

        string path = Path.Combine(wwwPath, "images");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var uploadedFiles = new Dictionary<string, string>();
        //var uploadedFiles = new List<string>();

        foreach (var file in files)
        {
            string fileName = string.Format("{0}-{1}", Guid.NewGuid(), file.FileName);
            var savePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                uploadedFiles.Add(fileName, savePath);
            }
        }
        return uploadedFiles;
    }



}