using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HakunaMatata.Controllers
{

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
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = _realEstateServices.GetById(id);
            return View(details);
        }
    }
}