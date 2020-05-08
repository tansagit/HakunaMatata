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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = _realEstateServices.GetById(id);
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
            //details.Files = files;
            int uploadedFiles = 0;
            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", details.RealEstateTypeId);
            //use IFormFile Collection as parameter will not work here
            // so use HttpContext instead
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value ?? string.Empty;
                if (userId != string.Empty)
                {
                    var realEstateId = _realEstateServices.AddCompleteRealEstate(details, Convert.ToInt32(userId));

                    //tao real estate thanh cong
                    if (realEstateId != -1)
                    {
                        if (details.Files.Count > 0)
                        {
                            uploadedFiles = _fileServices.AddPicture(realEstateId, details.Files);
                        }

                        //use tempdate pass message to index controller
                        TempData["Message"] = string.Format("Thêm phòng trọ thành công, uploaded {0} hình ảnh", uploadedFiles);
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var details = _realEstateServices.GetById(id);

            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", details.RealEstateTypeId);
            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //hien tai chua update list image, longtitude, latitude
        public IActionResult Edit(int id,
            [Bind("Id,Title,Address,Price,Acreage,ExprireTime,RoomNumber,Description,HasPrivateWc,HasMezzanine,AllowCook,FreeTime,SecurityCamera,WaterPrice,ElectronicPrice,WifiPrice,RealEstateTypeId")] VM_RealEstateDetails details)
        {
            if (id != details.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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


        [HttpPost, ActionName("Disable")]
        [ValidateAntiForgeryToken]
        public IActionResult DisableConfirm(int id)
        {
            var isSuccess = _realEstateServices.DisableRealEstate(id);
            return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllRealEstates", _realEstateServices.GetList()) });
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Test(VM_RealEstateDetails details)
        {
            var files = HttpContext.Request.Form.Files.ToList();
            var files2 = details.Files;
            if (files.Count > 0)
            {
                var uploadedFiles = _fileServices.AddPicture(23, files);
            }
            return View(files);
        }

    }
}