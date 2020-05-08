using HakunaMatata.Data;
using HakunaMatata.Helpers;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HakunaMatata.Services
{
    public interface IRealEstateServices
    {
        List<VM_RealEstate> GetList();
        VM_RealEstateDetails GetById(int? id);
        int AddNewRealEstate(RealEstate realEstate);
        bool AddRealEstateDetails(RealEstateDetail details);
        bool AddMapForRealEstate(Map map);
        int AddCompleteRealEstate(VM_RealEstateDetails details, int agentId);
        bool UpdateRealEstate(VM_RealEstateDetails details);
        void DeleteRealEstate(int id);
        bool DisableRealEstate(int id);
        void ActiveRealEstate(int id);
        IEnumerable<RealEstateType> GetRealEstateTypeList();
        IEnumerable<City> GetCityList();
        IEnumerable<District> GetDistrictList();

        bool IsExistRealEstate(int id);

        Tuple<int?, int?, int> GetLocation(string address);

        Task<List<VM_Search_Result>> SearchResults(VM_Search search);
        IQueryable<VM_Search_Result> GetRealEstateList(VM_Search search);


    }

    public class RealEstateServices : IRealEstateServices
    {
        private readonly HakunaMatataContext _context;
        public RealEstateServices(HakunaMatataContext context)
        {
            _context = context;
        }


        public List<VM_RealEstate> GetList()
        {
            var list = new List<VM_RealEstate>();
            var details = _context.RealEstateDetail
                                 .Include(detail => detail.RealEstate)
                                    .ThenInclude(detail => detail.Agent)
                                .Include(detail => detail.RealEstate)
                                    .ThenInclude(detail => detail.ReaEstateType)
                                .ToList();

            foreach (var item in details)
            {
                var vm_rt = new VM_RealEstate()
                {
                    Id = item.RealEstate.Id,
                    Title = item.Title,
                    ExprireTime = item.RealEstate.ExprireTime,
                    RealEstateTypeId = item.RealEstate.ReaEstateType.Id,
                    Price = item.Price,
                    AgentName = item.RealEstate.Agent.AgentName,
                    IsActive = item.RealEstate.IsActive
                };

                list.Add(vm_rt);
            }
            return list;
        }

        public VM_RealEstateDetails GetById(int? id)
        {
            var details = _context.RealEstateDetail
                .Where(x => x.RealEstateId == id)
                .Include(detail => detail.RealEstate)
                    .ThenInclude(detail => detail.Agent)
                .Include(detail => detail.RealEstate)
                    .ThenInclude(detail => detail.ReaEstateType)
                .SingleOrDefault();

            var map = _context.Map.Where(m => m.RealEstateId == id).SingleOrDefault();

            var images = _context.Picture
                .Where(pic => pic.RealEstateId == id && pic.IsActive == true)
                .ToList();

            var imgUrls = new List<string>();
            foreach (var img in images)
            {
                imgUrls.Add(img.Url);
            }

            if (details != null)
            {
                if (map == null)
                {
                    map = new Map();
                }

                var vm_rt_details = new VM_RealEstateDetails()
                {
                    Id = details.RealEstate.Id,
                    Title = details.Title,
                    Address = map.Address ?? string.Empty,
                    Price = details.Price,
                    Acreage = details.Acreage,
                    PostTime = details.RealEstate.PostTime,
                    LastUpdate = details.RealEstate.LastUpdate,
                    ExprireTime = details.RealEstate.ExprireTime?.ToString("dd/MM/yyyy"),
                    RoomNumber = details.RoomNumber,
                    Description = details.Description,
                    AgentName = details.RealEstate.Agent.AgentName,
                    HasPrivateWc = details.HasPrivateWc,
                    HasMezzanine = details.HasMezzanine,
                    AllowCook = details.AllowCook,
                    FreeTime = details.FreeTime,
                    SecurityCamera = details.SecurityCamera,
                    WaterPrice = details.WaterPrice == null ? 0 : details.WaterPrice,
                    ElectronicPrice = details.ElectronicPrice == null ? 0 : details.ElectronicPrice,
                    WifiPrice = details.WifiPrice,
                    Latitude = map.Latitude,
                    Longtitude = map.Longtitude,
                    RealEstateTypeId = details.RealEstate.RealEstateTypeId,
                    IsActive = details.RealEstate.IsActive,
                    ImageUrls = imgUrls
                };
                return vm_rt_details;
            }

            return new VM_RealEstateDetails();
        }

        public int AddNewRealEstate(RealEstate realEstate)
        {
            try
            {
                _context.RealEstate.Add(realEstate);
                _context.SaveChanges();
                return realEstate.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool AddRealEstateDetails(RealEstateDetail details)
        {
            try
            {
                _context.RealEstateDetail.Add(details);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddMapForRealEstate(Map map)
        {
            try
            {
                var location = GetLocation(map.Address);
                map.WardId = location.Item1;
                map.DistrictId = location.Item2;
                map.CityId = location.Item3;

                _context.Map.Add(map);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public int AddCompleteRealEstate(VM_RealEstateDetails details, int agentId)
        {
            var realEstate = new RealEstate()
            {
                PostTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                ExprireTime = Convert.ToDateTime(details.ExprireTime),
                RealEstateTypeId = details.RealEstateTypeId,
                AgentId = agentId,
                IsActive = false
            };
            var realEstateId = AddNewRealEstate(realEstate);

            //tao real estate thanh cong
            if (realEstateId != -1)
            {
                //add details
                var rtDetails = new RealEstateDetail()
                {
                    RealEstateId = realEstateId,
                    Title = details.Title,
                    Price = details.Price,
                    Acreage = details.Acreage,
                    RoomNumber = details.RoomNumber,
                    Description = details.Description,
                    HasPrivateWc = details.HasPrivateWc,
                    HasMezzanine = details.HasMezzanine,
                    AllowCook = details.AllowCook,
                    FreeTime = details.FreeTime,
                    SecurityCamera = details.SecurityCamera,
                    WaterPrice = details.isFreeWater ? 0 : Convert.ToInt32(details.WaterPrice),
                    ElectronicPrice = details.isFreeElectronic ? 0 : Convert.ToInt32(details.ElectronicPrice),
                    WifiPrice = details.isFreeWifi ? 0 : details.WifiPrice
                };

                var isSuccessAddDetails = AddRealEstateDetails(rtDetails);

                //add map
                var map = new Map()
                {
                    Address = details.Address,
                    Latitude = details.Latitude,
                    Longtitude = details.Longtitude,
                    RealEstateId = realEstateId
                };
                var isSuccessAddMap = AddMapForRealEstate(map);

                //neu add thong tin chi tiet va map thanh cong thi active
                if (isSuccessAddDetails && isSuccessAddMap)
                {
                    ActiveRealEstate(realEstateId);
                    return realEstateId;
                }
                else
                {
                    return -1;
                }
            }

            return realEstateId;
        }

        public bool UpdateRealEstate(VM_RealEstateDetails details)
        {
            var rt = _context.RealEstate.Find(details.Id);
            if (rt != null)
            {
                var rt_detail = _context.RealEstateDetail.FirstOrDefault(d => d.RealEstateId == rt.Id);
                var map = _context.Map.FirstOrDefault(m => m.RealEstateId == rt.Id);

                if (rt_detail != null && map != null)
                {
                    //it's update time
                    rt.LastUpdate = DateTime.Now;
                    rt.ExprireTime = DateTime.Parse(details.ExprireTime);
                    rt.RealEstateTypeId = details.RealEstateTypeId;

                    rt_detail.Title = details.Title;
                    rt_detail.Price = details.Price;
                    rt_detail.Acreage = details.Acreage;
                    rt_detail.RoomNumber = details.RoomNumber;
                    rt_detail.Description = details.Description;
                    rt_detail.HasPrivateWc = details.HasPrivateWc;
                    rt_detail.HasMezzanine = details.HasMezzanine;
                    rt_detail.AllowCook = details.AllowCook;
                    rt_detail.FreeTime = details.FreeTime;
                    rt_detail.SecurityCamera = details.SecurityCamera;
                    rt_detail.WaterPrice = Convert.ToInt32(details.WaterPrice);
                    rt_detail.ElectronicPrice = Convert.ToInt32(details.ElectronicPrice);
                    rt_detail.WifiPrice = details.WifiPrice;

                    map.Address = details.Address;

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else return false;
        }

        public void DeleteRealEstate(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool DisableRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && realEstate.IsActive)
            {
                realEstate.IsActive = false;
                realEstate.LastUpdate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void ActiveRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && !realEstate.IsActive)
            {
                realEstate.IsActive = true;
                _context.SaveChanges();

            }
        }

        public bool IsExistRealEstate(int id)
        {
            return _context.RealEstate.Any(r => r.Id == id);
        }

        public IEnumerable<RealEstateType> GetRealEstateTypeList()
        {
            return _context.RealEstateType.ToList();
        }

        public IEnumerable<City> GetCityList()
        {
            return _context.City.ToList();
        }

        public IEnumerable<District> GetDistrictList()
        {
            return _context.District.ToList();
        }

        /// <summary>
        /// Tim kiem phuong, quan, thanh pho tu dia chi
        /// </summary>
        /// <param name="address"></param>
        /// <returns>id cua phuong, quan va thanh pho</returns>
        public Tuple<int?, int?, int> GetLocation(string address)
        {
            int cityId = 1;
            int? districtId = null;
            int? wardId = null;


            var cityList = _context.City.ToList();

            foreach (var city in cityList)
            {
                if (address.Contains(city.CityName))
                {
                    cityId = city.Id;
                    break;
                }
            }

            var districtList = _context.District.Where(d => d.CityId == cityId).ToList();
            if (districtList.Count > 0)
            {
                foreach (var district in districtList)
                {
                    if (address.Contains(district.DistrictName))
                    {
                        districtId = district.Id;
                        break;
                    }
                }
                var wardList = _context.Ward.Where(w => w.DistrictId == districtId).ToList();
                if (wardList.Count > 0)
                {
                    foreach (var ward in wardList)
                    {
                        if (address.Contains(ward.WardName))
                        {
                            wardId = ward.Id;
                            break;
                        }
                    }
                }
            }
            var result = Tuple.Create(wardId, districtId, cityId);
            return result;
        }




        /*
         ------------Client services-------------------------------
         */
        public async Task<List<VM_Search_Result>> SearchResults(VM_Search search)
        {
            try
            {
                var finalResult = new List<VM_Search_Result>();

                var searchResult = await _context.RealEstateDetail
                                       .Include(r => r.RealEstate)
                                            .ThenInclude(r => r.Map)
                                        .Include(r => r.RealEstate)
                                            .ThenInclude(r => r.Picture)
                                        .Include(r => r.RealEstate)
                                            .ThenInclude(r => r.Agent)
                                        .Where(r => r.RealEstate.IsActive == true)
                                        .AsQueryable().ToListAsync();



                if (search != null)
                {
                    if (search.Type > 0)
                        searchResult = searchResult.Where(x => x.RealEstate.RealEstateTypeId == search.Type).ToList();
                    if (search.City > 0)
                        searchResult = searchResult.Where(x => x.RealEstate.Map.CityId == search.City).ToList();
                    if (search.District > 0)
                        searchResult = searchResult.Where(x => x.RealEstate.Map.DistrictId == search.District).ToList();

                    var priceRange = Helper.GetPriceRange(search.PriceRange);
                    var minPrice = priceRange[0];
                    var maxPrice = priceRange[1];
                    searchResult = searchResult.Where(
                        x => x.Price >= minPrice && x.Price <= maxPrice).ToList();

                    var acreageRange = Helper.GetAcreageRange(search.AcreageRange);
                    var minAcreage = acreageRange[0];
                    var maxAcreage = acreageRange[1];

                    searchResult = searchResult.Where(
                        x => x.Acreage >= minPrice && x.Acreage <= maxAcreage).ToList();

                    if (!String.IsNullOrEmpty(search.SearchString))
                        searchResult = searchResult.Where(
                            x => x.RealEstate.Map.Address.Contains(search.SearchString)).ToList();

                    searchResult = searchResult.OrderByDescending(x => x.RealEstate.PostTime).ToList();

                    finalResult = Helper.MapperToVMSearchResult(searchResult);
                }
                return finalResult;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<VM_Search_Result> GetRealEstateList(VM_Search condition)
        {
            try
            {
                var allPosts = _context.RealEstate.Where(r => r.IsActive == true)
                                    .Select(e => new
                                    {
                                        e, //real estate
                                        e.RealEstateDetail,
                                        e.ReaEstateType,
                                        e.Map,
                                        image = e.Picture.Where(p => p.IsActive == true).FirstOrDefault(),
                                        e.Agent
                                    });


                //filter
                if (allPosts != null)
                {
                    if (condition.Type > 0)
                        allPosts = allPosts.Where(x => x.ReaEstateType.Id == condition.Type);
                    if (condition.City > 0)
                        allPosts = allPosts.Where(x => x.Map.CityId == condition.City);
                    if (condition.District > 0)
                        allPosts = allPosts.Where(x => x.Map.DistrictId == condition.District);

                    var priceRange = Helper.GetPriceRange(condition.PriceRange);
                    var minPrice = priceRange[0];
                    var maxPrice = priceRange[1];
                    allPosts = allPosts.Where(x =>
                        x.RealEstateDetail.Price >= minPrice && x.RealEstateDetail.Price <= maxPrice);

                    var acreageRange = Helper.GetAcreageRange(condition.AcreageRange);
                    var minAcreage = acreageRange[0];
                    var maxAcreage = acreageRange[1];

                    allPosts = allPosts.Where(x =>
                        x.RealEstateDetail.Acreage >= minAcreage && x.RealEstateDetail.Acreage <= maxAcreage);

                    if (!String.IsNullOrEmpty(condition.SearchString))
                        allPosts = allPosts.Where(
                            x => x.Map.Address.Contains(condition.SearchString));

                    allPosts = allPosts.OrderByDescending(x => x.e.PostTime);
                }

                IQueryable<VM_Search_Result> results = (from item in allPosts
                                                        select new VM_Search_Result
                                                        {
                                                            Id = item.e.Id,
                                                            Street = Helper.GetStreet(item.Map.Address),
                                                            Price = item.RealEstateDetail.Price,
                                                            Acreage = item.RealEstateDetail.Acreage,
                                                            Type = item.ReaEstateType.Id,
                                                            PostTime = item.e.PostTime.ToString("dd/MM/yyyy"),
                                                            ImageUrl = GetLinkImage(item.image),
                                                            AgentName = item.Agent.AgentName
                                                        });

                if (allPosts.Count() > 0)
                {
                    #region commemed

                    //foreach (var item in allPosts)
                    //{
                    //    string imageUrl = string.Empty;
                    //    //neu list picture null hoac rong thi anh dai dien = 404
                    //    if (item.image == null)
                    //    {
                    //        imageUrl = "404";
                    //    }
                    //    else
                    //    {
                    //        //lay phan tu dau tien trong list picture

                    //        //kiem tra picture name no ton tai ko, 
                    //        // neu co nghia la moi them vao, ko phải crawl tu web
                    //        if (!string.IsNullOrEmpty(item.image.PictureName))
                    //        {
                    //            //tao url = cach + chuoi ~/images/ + PictureName
                    //            imageUrl = "local" + item.image.PictureName;
                    //        }
                    //        else
                    //        {
                    //            imageUrl = item.image.Url;
                    //        }
                    //    }

                    //    var resultItem = new VM_Search_Result()
                    //    {
                    //        Id = item.e.Id,
                    //        Street = Helper.GetStreet(item.Map.Address),
                    //        Price = item.RealEstateDetail.Price,
                    //        Acreage = item.RealEstateDetail.Acreage,
                    //        Type = item.ReaEstateType.Id,
                    //        PostTime = item.e.PostTime.ToString("dd/MM/yyyy"),
                    //        ImageUrl = imageUrl,
                    //        AgentName = item.Agent.AgentName
                    //    };
                    //    results.Add(resultItem);
                    //}
                    #endregion
                }
                return results;
            }
            catch
            {
                return null;
            }
        }
        private static string GetLinkImage(Picture image)
        {
            string imageUrl = string.Empty;
            if (image == null)
            {
                imageUrl = "404";
            }
            else
            {
                //lay phan tu dau tien trong list picture

                //kiem tra picture name no ton tai ko, 
                // neu co nghia la moi them vao, ko phải crawl tu web
                if (!string.IsNullOrEmpty(image.PictureName))
                {
                    //tao url = cach + chuoi ~/images/ + PictureName
                    imageUrl = "local" + image.PictureName;
                }
                else
                {
                    imageUrl = image.Url;
                }
            }
            return imageUrl;
        }
    }

}
