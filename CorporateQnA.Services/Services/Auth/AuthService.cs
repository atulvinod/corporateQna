using CorporateQnA.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

            if(getUser == null)
            {
                return false;
            }

            var signinResult = await this.signInManager.PasswordSignInAsync(getUser, password, false, false);
            if (signinResult.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<List<string>> Register(string name, string username, string email, string password, string location, string position, string department)
        {
            List<string> errors = new List<string>();
            var user = await this.userManager.FindByEmailAsync(email);

            //if the user already exits
            if (user != null)
            {
                errors.Add("User already exists");
                return errors; ;
            }

            var appUser = new Users
            {
                Department = department,
                Location = location,
                Email = email,
                Name = name,
                Position = position
            };

            var validators = this.userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var passwordResult = await validator.ValidateAsync(this.userManager, null, password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        errors.Add(error.Description);
                    }
                }
            }
            //check if password validation didnt add any errors
            if(errors.Count != 0)
            {
                return errors;
            }

            var userId = this.userService.Create(appUser);
            var newUser = new AppIdentityUser
            {
                UserId = userId,
                UserName = username,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(newUser, false);
                return null;
            }
            else
            {
                foreach(var i in result.Errors)
                {
                    errors.Add(i.Description);
                }
            }

            return errors;
        }

        public async Task<string> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutRequest = await this.interactionService.GetLogoutContextAsync(logoutId);
            return logoutRequest.PostLogoutRedirectUri;
        }
    }
}
