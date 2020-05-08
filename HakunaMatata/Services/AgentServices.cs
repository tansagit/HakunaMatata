using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IAgentServices
{
    Task<IEnumerable<Agent>> GetListAgent();
    Task<VM_Agent> GetDetails(int id);
    Task<bool> Disable(int id);
}

public class AgentServices : IAgentServices
{
    private readonly HakunaMatataContext _context;
    public AgentServices(HakunaMatataContext context)
    {
        _context = context;
    }
    public async Task<bool> Disable(int id)
    {
        var agent = await _context.Agent.FindAsync(id);
        if (agent != null)
        {
            if (!agent.IsActive) return false;
            agent.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
        else return false;
    }

    public async Task<VM_Agent> GetDetails(int id)
    {
        var agent = await _context.Agent.FindAsync(id);
        if (agent != null)
        {
            var posts = await _context.RealEstate
                                .Include(r => r.RealEstateDetail)
                                .Include(r => r.Map)
                                .Where(r => r.AgentId == agent.Id)
                                .ToListAsync();

            int total = posts.Count();
            int activePosts = posts.Where(p => p.IsActive == true).ToList().Count;
            var details = new VM_Agent()
            {
                Id = agent.Id,
                Name = agent.AgentName,
                ContactNumber = agent.PhoneNumber,
                TotalPosts = total,
                ActivePosts = activePosts,
                IsActive = agent.IsActive,
                IsConfirmedNumber = agent.ConfirmPhoneNumber,
                Posts = posts
            };
            return details;
        }
        return null;
    }

    public async Task<IEnumerable<Agent>> GetListAgent()
    {
        return await _context.Agent
            .Include(a => a.Level)
            .ToListAsync();
    }
}
