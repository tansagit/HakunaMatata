using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakunaMatata.Helpers;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsServices _services;
        public AboutUsController(IAboutUsServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var list = _services.GetListAboutUs();
            return View(list);
        }

        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new AboutUs());
            }
            else
            {
                var about = _services.GetDetails(id);
                if (about == null)
                {
                    return NotFound();
                }
                return View(about);
            }

        }

        [HttpPost]
        public IActionResult CreateOrEdit(int id, [Bind("Id,Content")]AboutUs about)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (id == 0)
                {
                    _services.Create(about);
                }

                //update
                else
                {
                    try
                    {
                        _services.UpdateAboutUs(about);
                    }
                    catch (Exception)
                    {
                        if (!_services.IsExist(about.Id))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllAboutUs", _services.GetListAboutUs()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", about) });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _services.DeleteAboutUs(id);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllAboutUs", _services.GetListAboutUs()) });
        }


    }
}