using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Ishant_Goyal.services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using static Ishant_Goyal.Utility;

namespace Ishant_Goyal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddDbContext<IshantContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ishant")));

            services.AddScoped<IUserservices, Userservices>();
            services.AddScoped<IEmailServices, Emailservices>();

            services.AddAuthentication("CookieAuthentication")
                  .AddCookie("CookieAuthentication", config =>
                  {
                      config.Cookie.Name = "UserCookie";
                      config.LoginPath = "/Registration/Login";
                      config.LogoutPath = "/Registration/Logout";
                  });

            var encrypt = Configuration
           .GetSection("Encryption")
           .Get<Encryption>();
            services.AddSingleton(encrypt);

            var emailC = Configuration
           .GetSection("EmailConfig")
           .Get<EmailConfig>();
            services.AddSingleton(emailC);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
