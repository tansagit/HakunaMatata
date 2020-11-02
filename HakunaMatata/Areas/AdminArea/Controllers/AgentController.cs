using HakunaMatata.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentServices _services;
        public AgentController(IAgentServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var agents = await _services.GetListAgent();
            return View(agents);
        }

        public async Task<IActionResult> Details(int id)
        {
            var agent = await _services.GetDetails(id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Disable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableConfirm(int id)
        {
            var isSuccess = _services.Disable(id);
            return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllAgents", await _services.GetListAgent()) });
        }
    }
}