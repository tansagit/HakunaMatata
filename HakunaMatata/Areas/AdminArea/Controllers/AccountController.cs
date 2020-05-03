using HakunaMatata.Helpers;
using HakunaMatata.Models.ViewModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Login(string returnUrl = "")
        {
            var model = new VM_Login { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(VM_Login account)
        {
            if (ModelState.IsValid)
            {
                var member = _services.GetUser(account);
                if (member != null)
                {
                    var userPrincipal = Helper.GenerateIdentity(member);

                    var props = new AuthenticationProperties();
                    props.IsPersistent = account.IsRememberMe;

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
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Invalid user or password!";
                }
            }
            return View(account);
        }

        public IActionResult Denied()
        {
            return View();
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