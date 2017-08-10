using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EComm.MVC.ViewModels
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (!ModelState.IsValid || lvm.Username != "test" || lvm.Password != "password") return View(lvm);

            var claims = new List<Claim>
                { new Claim(ClaimTypes.Name, lvm.Username), new Claim(ClaimTypes.Role, "User") };
            var identity = new ClaimsIdentity(claims, "FormsAuthentication");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (lvm.ReturnUrl != null) return LocalRedirect(lvm.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
