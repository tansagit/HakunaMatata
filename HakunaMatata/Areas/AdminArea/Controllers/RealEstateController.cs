using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Helpers;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _realEstateServices;
        private readonly IFileServices _fileServices;
        //public readonly List<IFormFile> UploadedFiles;

        public RealEstateController(IRealEstateServices realEstateServices, IFileServices fileServices)
        {
            _realEstateServices = realEstateServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var vm_realEstates = _realEstateServices.GetList();
            ViewBag.Message = TempData["Message"];
            return View(vm_realEstates);
        }

        public IActionResult Index2()
        {
            return View();
        }
        /// <summary>
        /// index 2 thu dung cach paging theo server side
        /// </summary>
        /// <param name="pageIndex">Pagination index</param>
        /// <returns></returns>
        public async Task<IActionResult> LoadData(int pageIndex, string searchKey)
        {
            var source = _realEstateServices.GetRealEstates(searchKey);
            var count = await source.CountAsync();
            // dua vao pageIndex de get ra tu source bao nhieu item
            // o day mac dinh lay 5 item 1 trang
            var items = await source.Skip((pageIndex - 1) * 20).Take(20).ToListAsync();

            return Json(new { data = items, totalRow = count });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _realEstateServices.GetRealEstateDetails(id);
            if (details == null)
            {
                return NotFound();
            }
            else
            {
                var pictures = await _fileServices.GetPicturesForRealEstate(details.Id);
                details.ImageUrls = Helper.GetImageUrls(pictures);
            }
            ViewData["GOOGLE_MAP_API"] = Constants.GOOGLE_MAP_MARKER_API;
            return View(details);
        }



        public IActionResult Create()
        {
            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", realEstateTypeList.First());
            return View();
        }


        [HttpPost]
        public IActionResult Create(
            [Bind("Title,Address,Price,Acreage,ExprireTime,RoomNumber,Description,HasPrivateWc,HasMezzanine,AllowCook,FreeTime,SecurityCamera,WaterPrice,ElectronicPrice,WifiPrice,RealEstateTypeId,Latitude,Longtitude,isFreeWater,isFreeElectronic,isFreeWifi,Files")]
            VM_RealEstateDetails details)
        {
            //get files from http request
            //var files = HttpContext.Request.Form.Files.ToList();


            int uploadedFilesCount = 0;
            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", details.RealEstateTypeId);

            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value ?? string.Empty;
                if (userId != string.Empty)
                {
                    var realEstateId = _realEstateServices.AddCompleteRealEstate(details, Convert.ToInt32(userId));

                    //tao real estate thanh cong
                    if (realEstateId != -1)
                    {
                        if (details.Files != null && details.Files.Count > 0)
                        {
                            uploadedFilesCount = _fileServices.AddPicture(realEstateId, details.Files);
                        }

                        //use tempdate pass message to index controller
                        TempData["Message"] = string.Format("Thêm phòng trọ thành công, uploaded {0} hình ảnh", uploadedFilesCount);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng thử lại";

                        return View(details);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "User id không hợp lệ";
                    return View(details);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng thử lại";
                return View(details);
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var details = _realEstateServices.GetById(id);
            var details = await _realEstateServices.GetRealEstateDetails(id);
            if (details == null)
            {
                return NotFound();
            }
            //else
            //{
            //    var pictures = await _fileServices.GetPicturesForRealEstate(details.Id);
            //    details.Pictures = pictures.ToList();
            //}

            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", details.RealEstateTypeId);
            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id,Title,Address,Price,Acreage,ExprireTime,RoomNumber,Description,HasPrivateWc,HasMezzanine,AllowCook,FreeTime,SecurityCamera,WaterPrice,ElectronicPrice,WifiPrice,RealEstateTypeId,Files")] VM_RealEstateDetails details)
        {
            int uploadedFilesCount = 0;
            var files = HttpContext.Request.Form.Files.ToList();

            if (id != details.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (details.Files != null && details.Files.Count > 0)
                    {
                        uploadedFilesCount = _fileServices.AddPicture(details.Id, details.Files);
                    }
                    var isUpdateSuccess = _realEstateServices.UpdateRealEstate(details);
                    if (isUpdateSuccess) return RedirectToAction(nameof(Index));
                    else return View(details);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_realEstateServices.IsExistRealEstate(details.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(details);
        }


        //[HttpPost, ActionName("Disable")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DisableConfirm(int id)
        //{
        //    var isSuccess = _realEstateServices.DisableRealEstate(id);
        //    return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllRealEstates", _realEstateServices.GetList()) });
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisableRealEsate(int id)
        {
            var status = _realEstateServices.DisableRealEstate(id);
            return Json(new { status });
        }


        //hình như ko dùng
        //[HttpPost, ActionName("RemovePicture")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RemovePictureConfirm(int id)
        //{
        //    int realEstateId = Convert.ToInt32(_fileServices.GetRealEstateId(id));
        //    var isSuccess = await _fileServices.RemovePictureFromRealEstate(id);

        //    return Json(new
        //    {
        //        isSuccess,
        //        html = Helper.RenderRazorViewToString(this, "_ViewPictures", _fileServices.GetPicturesForRealEstate(realEstateId))
        //    });
        //}

    }
}