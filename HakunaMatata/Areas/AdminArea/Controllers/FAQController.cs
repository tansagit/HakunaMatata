﻿using HakunaMatata.Helpers;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static HakunaMatata.Helpers.Helper;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin,Manager")]
    public class FAQController : Controller
    {
        private readonly IFAQServices _services;

        public FAQController(IFAQServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var faqs = _services.GetListFAQs();
            return View(faqs);
        }

        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Faq());
            }
            else
            {
                var faq = _services.GetDetails(id);
                if (faq == null)
                {
                    return NotFound();
                }
                return View(faq);
            }

        }

        [HttpPost]
        public IActionResult CreateOrEdit(int id, [Bind("Id,Question,Answer")]Faq faq)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (id == 0)
                {
                    _services.Create(faq);
                }

                //update
                else
                {
                    try
                    {
                        _services.UpdateFAQ(faq);
                    }
                    catch (Exception)
                    {
                        if (!_services.IsExist(faq.Id))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllFAQs", _services.GetListFAQs()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", faq) });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _services.DeleteFAQ(id);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllFAQs", _services.GetListFAQs()) });
        }
    }
}