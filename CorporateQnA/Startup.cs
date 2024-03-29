using CorporateQnA.Config;
using CorporateQnA.Data;
using CorporateQnA.Models;
using CorporateQnA.Services;
using CorporateQnA.Services.Auth;
using IdentityServer4;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();            
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

            IdentityServerConfig.InitializeIdentityServerConfig(services, configuration);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB"));
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";

                //set same site as unspecified to fix cookie rejection, might have to change this
                config.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;

                //the login path which serves the cookie
                config.LoginPath = "/Auth/Login";

                //to logout
                config.LogoutPath = "/Auth/Logout";

            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IdentityServerConfig.InitializeDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseDefaultFiles();

            app.UseStaticFiles();

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
