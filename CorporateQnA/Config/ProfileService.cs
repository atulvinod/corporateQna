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
            var userData = this.userService.GetUser(user.UserId);
            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims.Add(new Claim("Location", userData.Location));
            claims.Add(new Claim("Department", userData.Department));
            claims.Add(new Claim("Position", userData.Position));
            claims.Add(new Claim("Name", userData.Name));
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
