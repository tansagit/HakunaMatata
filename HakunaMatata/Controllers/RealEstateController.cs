using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Helpers;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> Index(int? page,
                                               int? type,
                                               int? city,
                                               int? district,
                                               int? priceRange,
                                               int? acreageRange,
                                               string search)
        {

            int pageSize = 5;
            var condition = new Condition()
            {
                Type = type ?? 0,
                City = city ?? 0,
                District = district ?? 0,
                PriceRange = priceRange ?? 0,
                AcreageRange = acreageRange ?? 0,
                SearchString = search ?? string.Empty
            };

            var source = _realEstateServices.Filter(condition);


            var types = _realEstateServices.GetRealEstateTypeList();
            types = types.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Tất cả" } });
            types = types.OrderBy(t => t.Id);

            var cities = _realEstateServices.GetCityList();
            cities = cities.Concat(new[] { new City { Id = 0, CityName = "Tất cả" } });
            cities = cities.OrderBy(c => c.Id);


            var districts = _realEstateServices.GetDistrictList();
            districts = districts.Concat(new[] { new District { Id = 0, DistrictName = "Tất cả" } });
            districts = districts.OrderBy(d => d.Id);

            var priceRanges = Helper.GetPriceRangeForView();
            var acreagaRange = Helper.GetAcreageRangeForView();

            ViewData["Types"] = new SelectList(types, "Id", "RealEstateTypeName", condition.Type);
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName", condition.City);
            ViewData["Districts"] = new SelectList(districts, "Id", "DistrictName", condition.District);
            ViewData["PriceRanges"] = new SelectList(priceRanges, "Value", "Key", condition.PriceRange);
            ViewData["AcreagaRanges"] = new SelectList(acreagaRange, "Value", "Key", condition.AcreageRange);

            return View(await CustomPagination.CreateAsync(source, condition, page ?? 1, pageSize));
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