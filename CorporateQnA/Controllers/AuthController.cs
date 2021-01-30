using CorporateQnA.Models.View;
using CorporateQnA.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(model: new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            // TODO: If the modelstate is invalid, return errors
            if (ModelState.IsValid == false)
            {
                return View(model: login);
            }

            var result = await this.authService.Login(login.Email, login.Password);

            // show incorrect errors
            if (result == false)
            {
                ViewBag.Fail = "Incorrect Email or password";
                return View(model: login);
            }

            return Redirect(login.ReturnUrl); ;
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutID)
        {
            return Redirect(await this.authService.Logout(logoutID));
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register)
        {

            //check model state
            if (ModelState.IsValid == false)
            {
                return View(model: register);
            }

            var errors = await this.authService.Register(register.Username, register.Username, register.Email, register.Password, register.Location, register.Position, register.Department);

            if (errors != null)
            {
                ViewBag.Errors = errors;
                return View(model: register);
            }

            return Redirect(register.ReturnUrl);
        }
    }
}
