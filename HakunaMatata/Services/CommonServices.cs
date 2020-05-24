using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface ICommonServices
{
    Task<IEnumerable<District>> GetDistrictsByCity(int? cityId);
}

public class CommonServices : ICommonServices
{
    private readonly HakunaMatataContext _context;
    public CommonServices(HakunaMatataContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<District>> GetDistrictsByCity(int? cityId)
    {
        return await _context.District.Where(d => d.CityId == cityId).ToListAsync();
    }
}

