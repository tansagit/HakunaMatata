using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PictureController : Controller
    {
        private readonly IFileServices _services;
        public PictureController(IFileServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoadData(int realEstateId)
        {
            var pictures = _services.GetPicturesForRealEstate(realEstateId);

            if (pictures != null)
            {
                return Json(new
                {
                    data = pictures,
                    status = true
                });
            }
            else return Json(new { status = false });
        }

        [HttpPost]
        public IActionResult Remove(int pictureId)
        {
            var status = _services.RemovePictureFromRealEstate(pictureId);
            return Json(new { status });
        }

    }
}