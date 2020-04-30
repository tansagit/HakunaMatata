using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Helpers;
using HakunaMatata.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _realEstateServices;
        private readonly IFileServices _fileServices;

        public RealEstateController(IRealEstateServices realEstateServices, IFileServices fileServices)
        {
            _realEstateServices = realEstateServices;
            _fileServices = fileServices;
        }

        public async Task<IActionResult> Index()
        {
            var vm_realEstates = await _realEstateServices.GetList();
            return View(vm_realEstates);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _realEstateServices.GetById(id);
            ViewData["GOOGLE_MAP_API"] = Constants.GOOGLE_MAP_MARKER_API;
            return View(details);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var details = await _realEstateServices.GetById(id);

            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["RealEstateTypeId"] = new SelectList(realEstateTypeList, "Id", "RealEstateTypeName", details.RealEstateTypeId);
            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //hien tai chua update list image, longtitude, latitude
        public async Task<IActionResult> Edit(int id,
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
                    var isUpdateSuccess = await _realEstateServices.UpdateRealEstate(details);
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


    }
}