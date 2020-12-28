using HakunaMatata.Helpers;
using HakunaMatata.Models.ViewModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HakunaMatata.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AccountController : Controller
    {
        private readonly IAccountServices _services;

        public AccountController(IAccountServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("dang-nhap")]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new VM_Login { ReturnUrl = returnUrl };
            ViewBag.RegisterMessage = TempData["RegisterMessage"];
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap")]
        public async Task<IActionResult> Login(VM_Login account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = _services.GetUser(account);
                    if (member != null)
                    {
                        var userPrincipal = Helper.GenerateIdentity(member);

                        var props = new AuthenticationProperties
                        {
                            IsPersistent = account.IsRememberMe
                        };

                        //sign in
                        await HttpContext.SignInAsync(
                            scheme: "MyCookieScheme",
                            principal: userPrincipal,
                            properties: props
                            );

                        if (!string.IsNullOrEmpty(account.ReturnUrl)
                            && Url.IsLocalUrl(account.ReturnUrl))
                            return Redirect(account.ReturnUrl);
                        else
                        {
                            //client
                            if (member.LevelId == 3)
                            {
                                return RedirectToAction("ClientRealEstateList", "RealEstate");
                            }
                            return RedirectToAction("CustomerConfirmList", "RealEstate");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Sai tài khoản hoặc mật khẩu!";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(account);
        }

        [Route("dang-ki")]
        public IActionResult Register(string returnUrl = "")
        {
            var model = new VM_Register { ReturnUrl = returnUrl };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ki")]
        public async Task<IActionResult> Register(VM_Register newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isSuccess = await _services.RegisterUser(newUser);
                    if (isSuccess)
                    {
                        TempData["RegisterMessage"] = "Đăng kí thành công, đăng nhập để tiếp tục.";
                        return RedirectToAction("Login", "Account");
                    }
                    else return View(newUser);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(newUser);
                }
            }
            return View(newUser);
        }

        public IActionResult Denied()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckExist(string loginName)
        {
            bool isExisted = _services.CheckExist(loginName);
            return Json(new { isExisted });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            scheme: "MyCookieScheme"
            );

            return RedirectToAction("Login");
        }
    }
}