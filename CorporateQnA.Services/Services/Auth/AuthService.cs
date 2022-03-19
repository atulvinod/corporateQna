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

        /// <summary>
        /// Initializes an instance of AuthService
        /// </summary>
        /// <param name="signInManager">The signin manager</param>
        /// <param name="userManager">The user manager</param>
        /// <param name="interactionService">Identity interaction service</param>
        /// <param name="userService">The user service</param>
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

        /// <summary>
        /// Initiates user login
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>true if user has successfully logged in</returns>
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

        /// <summary>
        /// Initiates user registration
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="username">The users' username</param>
        /// <param name="email">The email</param>
        /// <param name="password">The user password</param>
        /// <param name="location">The user location</param>
        /// <param name="position">The user position</param>
        /// <param name="department">The user department</param>
        /// <returns>null if successful registration, else validation errors</returns>
        public async Task<List<string>> Register(string name, string username, string email, string password, string location, string position, string department)
        {
            List<string> validationErrors = new();
            bool isUserAvailable = (await this.userManager.FindByEmailAsync(email)) != null || (await this.userManager.FindByNameAsync(username)) != null;

            //if the user already exits
            if (isUserAvailable)
            {
                validationErrors.Add("User already exists");
                return validationErrors;
            }

            var passwordValidators = this.userManager.PasswordValidators;

            foreach (var validator in passwordValidators)
            {
                var passwordResult = await validator.ValidateAsync(this.userManager, null, password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        validationErrors.Add(error.Description);
                    }
                }
            }
            //check if password validation didnt add any errors
            if(validationErrors.Count != 0)
            {
                return validationErrors;
            }

            var appUser = new Users
            {
                Department = department,
                Location = location,
                Email = email,
                Name = name,
                Position = position
            };

            var userId = this.userService.CreateUser(appUser);
            
            var newIdentityUser = new AppIdentityUser
            {
                UserId = userId,
                UserName = username,
                Email = email,
            };

            var createUserResult = await this.userManager.CreateAsync(newIdentityUser, password);

            if (createUserResult.Succeeded)
            {
                await this.signInManager.SignInAsync(newIdentityUser, false);
                return null;
            }
            else
            {
                foreach(var i in createUserResult.Errors)
                {
                    validationErrors.Add(i.Description);
                }
            }

            return validationErrors;
        }

        /// <summary>
        /// Initiates user logout
        /// </summary>
        /// <param name="logoutId">The logout id</param>
        /// <returns>The post logout redirect uri</returns>
        public async Task<string> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutRequest = await this.interactionService.GetLogoutContextAsync(logoutId);
            return logoutRequest.PostLogoutRedirectUri;
        }
    }
}
