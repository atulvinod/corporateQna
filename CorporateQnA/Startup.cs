using AutoMapper;
using CorporateQnA.Config;
using CorporateQnA.Data;
using CorporateQnA.Models;
using CorporateQnA.Services;
using CorporateQnA.Services.Auth;
using CorporateQnA.Services.ModelMaps;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddLocalApiAuthentication();

            services.AddIdentity<AppIdentityUser, IdentityRole>(options =>
            {
                //email should be unique
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

            services
              .AddIdentityServer()
              .AddAspNetIdentity<AppIdentityUser>()
              .AddInMemoryApiResources(IdentityServerConfig.GetApis())
              .AddInMemoryClients(IdentityServerConfig.GetClients())
              .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
              .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
              .AddDeveloperSigningCredential();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB"));
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                //the login path which serves the cookie
                config.LoginPath = "/Auth/Login";

                //to logout
                config.LogoutPath = "/Auth/Logout";
            });

            services.AddAutoMapper(typeof(AnswerMap), typeof(QuestionMap), typeof(CategoryMap));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActivityService, ActivityService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
