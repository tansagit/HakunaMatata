using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IRealEstateServices
{
    Task<List<VM_RealEstate>> GetList();
    Task<VM_RealEstateDetails> GetById(int? id);
    Task<int> AddNewRealEstate(RealEstate realEstate);
    Task<bool> UpdateRealEstate(VM_RealEstateDetails details);
    Task DeleteRealEstate(int id);
    Task<bool> DisableRealEstate(int id);
    IEnumerable<RealEstateType> GetRealEstateTypeList();
    bool IsExistRealEstate(int id);
}

public class RealEstateServices : IRealEstateServices
{
    private readonly HakunaMatataContext _context;
    public RealEstateServices(HakunaMatataContext context)
    {
        _context = context;
    }


    public async Task<List<VM_RealEstate>> GetList()
    {
        var list = new List<VM_RealEstate>();
        var details = await _context.RealEstateDetail
                             .Include(detail => detail.RealEstate)
                                .ThenInclude(detail => detail.Agent)
                            .Include(detail => detail.RealEstate)
                                .ThenInclude(detail => detail.ReaEstateType)
                            .ToListAsync();

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

    public async Task<VM_RealEstateDetails> GetById(int? id)
    {
        var details = await _context.RealEstateDetail
            .Where(x => x.RealEstateId == id)
            .Include(detail => detail.RealEstate)
                .ThenInclude(detail => detail.Agent)
            .Include(detail => detail.RealEstate)
                .ThenInclude(detail => detail.ReaEstateType)
            .SingleOrDefaultAsync();

        var map = await _context.Map.Where(m => m.RealEstateId == id).SingleOrDefaultAsync();

        var images = await _context.Picture
            .Where(pic => pic.RealEstateId == id && pic.IsActive == true)
            .ToListAsync();
        var imgUrls = new List<string>();
        foreach (var img in images)
        {
            imgUrls.Add(img.Url);
        }

        if (details != null)
        {
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

    public Task<int> AddNewRealEstate(RealEstate realEstate)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteRealEstate(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DisableRealEstate(int id)
    {
        throw new System.NotImplementedException();
    }





    public bool IsExistRealEstate(int id)
    {
        return _context.RealEstate.Any(r => r.Id == id);
    }

    public async Task<bool> UpdateRealEstate(VM_RealEstateDetails details)
    {
        var rt = await _context.RealEstate.FindAsync(details.Id);
        if (rt != null)
        {
            var rt_detail = await _context.RealEstateDetail.FirstOrDefaultAsync(d => d.RealEstateId == rt.Id);
            var map = await _context.Map.FirstOrDefaultAsync(m => m.RealEstateId == rt.Id);

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

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        else return false;
    }

    public IEnumerable<RealEstateType> GetRealEstateTypeList()
    {
        return _context.RealEstateType.ToList();
    }
}
