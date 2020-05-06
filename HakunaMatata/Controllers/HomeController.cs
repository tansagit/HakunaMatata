using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HakunaMatata.Models;
using HakunaMatata.Helpers;
using HakunaMatata.Models.ViewModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using HakunaMatata.Models.DataModels;

namespace HakunaMatata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRealEstateServices _services;
        public HomeController(ILogger<HomeController> logger, IRealEstateServices services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            var cityList = _services.GetCityList();
            cityList = cityList.Concat(new[] { new City { Id = 0, CityName = "Tất cả" } });
            cityList = cityList.OrderBy(c => c.Id);

            var typeList = _services.GetRealEstateTypeList();
            typeList = typeList.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Tất cả" } });
            typeList = typeList.OrderBy(t => t.Id);

            ViewData["Cities"] = new SelectList(cityList, "Id", "CityName");
            ViewData["Types"] = new SelectList(typeList, "Id", "RealEstateTypeName");
            return View();
        }

        public async Task<IActionResult> Index2(VM_Search search, int? pageNumber)
        {
            int pageSize = 10;
            var results = _services.GetRealEstateList(search);
            return View(await PaginatedList<VM_Search_Result>.CreateAsync(results, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Search(VM_Search search)
        {
            var results = await _services.SearchResults(search);

            var viewmodel = new VM_Search_Result_Container();
            viewmodel.SearchObject = search;
            viewmodel.ResultList = results;

            var types = _services.GetRealEstateTypeList();
            types = types.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Tất cả" } });
            types = types.OrderBy(t => t.Id);

            var cities = _services.GetCityList();
            cities = cities.Concat(new[] { new City { Id = 0, CityName = "Tất cả" } });
            cities = cities.OrderBy(c => c.Id);


            var districts = _services.GetDistrictList();
            districts = districts.Concat(new[] { new District { Id = 0, DistrictName = "Tất cả" } });
            districts = districts.OrderBy(d => d.Id);

            var priceRanges = Helper.GetPriceRangeForView();
            var acreagaRange = Helper.GetAcreageRangeForView();


            ViewData["Types"] = new SelectList(types, "Id", "RealEstateTypeName", search.Type);
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName", search.City);
            ViewData["Districts"] = new SelectList(districts, "Id", "DistrictName", search.District);
            ViewData["PriceRanges"] = new SelectList(priceRanges, "Value", "Key", search.PriceRange);
            ViewData["AcreagaRanges"] = new SelectList(acreagaRange, "Value", "Key", search.AcreageRange);
            return View(viewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(VM_Search_Result_Container container)
        {
            var result = await _services.SearchResults(container.SearchObject);

            container.ResultList = result;

            var types = _services.GetRealEstateTypeList();
            types = types.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Tất cả" } });
            types = types.OrderBy(t => t.Id);

            var cities = _services.GetCityList();
            cities = cities.Concat(new[] { new City { Id = 0, CityName = "Tất cả" } });
            cities = cities.OrderBy(c => c.Id);


            var districts = _services.GetDistrictList();
            districts = districts.Concat(new[] { new District { Id = 0, DistrictName = "Tất cả" } });
            districts = districts.OrderBy(d => d.Id);

            var priceRanges = Helper.GetPriceRangeForView();
            var acreagaRange = Helper.GetAcreageRangeForView();

            ViewData["Types"] = new SelectList(types, "Id", "RealEstateTypeName", container.SearchObject.Type);
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName", container.SearchObject.City);
            ViewData["Districts"] = new SelectList(districts, "Id", "DistrictName", container.SearchObject.District);
            ViewData["PriceRanges"] = new SelectList(priceRanges, "Value", "Key", container.SearchObject.PriceRange);
            ViewData["AcreagaRanges"] = new SelectList(acreagaRange, "Value", "Key", container.SearchObject.AcreageRange);

            return View(container);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
