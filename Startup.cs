using client_server.Data.Models;
using client_server.Models;
using client_server.Services;
using client_server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_server
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
      services.AddControllersWithViews();

      services.AddDbContext<ApplicationDBContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

      services.AddTransient<IUserService, UserService>();
      services.AddIdentity<UsersModel, IdentityRole>(options =>
        {
          options.Password.RequiredLength = 6;
          options.Password.RequireDigit = false;
          options.Password.RequireLowercase = false;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => {
          options.Cookie.Name = "ApplicationCookie";
          options.LoginPath = "/User/Login";
          options.LogoutPath = "/User/Logout";
        });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection()
         .UseStaticFiles()
         .UseRouting()
         .UseCookiePolicy();

      app.UseAuthorization();
      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Comments}/{action=Index}/{id?}");
      });
    }
  }
}
