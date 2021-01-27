using CorporateQnA.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityServerInteractionService interactionService;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
              IIdentityServerInteractionService interactionService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.interactionService = interactionService;
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

            var newUser = new ApplicationUser
            {
                Email = email,
                UserName = username,
                Name = name,
                Location = location,
                Department = department,
                Position = position
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
