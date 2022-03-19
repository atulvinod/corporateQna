using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Services;
using CorporateQnA.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Extensions;
using System.Security.Claims;

namespace CorporateQnA.Config
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService userService;
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly IUserClaimsPrincipalFactory<AppIdentityUser> userClaimsPrincipalFactory;

        public ProfileService(IUserService userService, UserManager<AppIdentityUser> userManager, IUserClaimsPrincipalFactory<AppIdentityUser> userClaimsPrincipalFactory)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await this.userManager.FindByIdAsync(sub);
            var userData = this.userService.GetUserById(user.UserId);
            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims.Add(new Claim("location", userData.Location));
            claims.Add(new Claim("department", userData.Department));
            claims.Add(new Claim("position", userData.Position));
            claims.Add(new Claim("name", userData.Name));
            claims.Add(new Claim("userId", userData.Id.ToString()));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
