using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetzweltExam.Models;
using NetzweltExam.Services;

namespace NetzweltExam.Controllers
{
    public class AuthController : Controller
    {
        private readonly INetzweltService _netzweltService;

        public AuthController(INetzweltService netzweltService)
        {
            _netzweltService = netzweltService;
        }
        
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _netzweltService.GetUser(model);

            if (user == null)
            {
                ModelState.AddModelError("InvalidCredentials","Invalid Username and/or Password.");
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Territory", "Home");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}