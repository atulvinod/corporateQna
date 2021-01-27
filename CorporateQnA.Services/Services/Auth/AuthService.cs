using CorporateQnA.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<AppIdentityUser> signInManager;
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly IIdentityServerInteractionService interactionService;
        private readonly IUserService userService;

        public AuthService(SignInManager<AppIdentityUser> signInManager, UserManager<AppIdentityUser> userManager,
              IIdentityServerInteractionService interactionService, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.interactionService = interactionService;
            this.userService = userService;
            this.userManager = userManager;
            this.interactionService = interactionService;
            this.signInManager = signInManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            var getUser = await this.userManager.FindByEmailAsync(email);
            var signinResult = await this.signInManager.PasswordSignInAsync(getUser, password, false, false);
            if (signinResult.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Register(string name, string username, string email, string password, string location, string position, string department)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            //if the user already exits
            if (user != null)
            {
                return false;
            }

            var appUser = new AppUser
            {
                Department = department,
                Location = location,
                Email = email,
                Name = name
            };

            var userId = this.userService.Create(appUser);
            var newUser = new AppIdentityUser
            {
                UserId = userId,
                Email = email,
                UserName = username
            };

            var result = await this.userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(newUser, false);
                return true;
            }

            return false;
        }

        public async Task<string> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutRequest = await this.interactionService.GetLogoutContextAsync(logoutId);
            return logoutRequest.PostLogoutRedirectUri;
        }
    }
}
