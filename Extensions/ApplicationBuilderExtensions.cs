using client_server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace client_server.Extensions
{
  public static class ApplicationBuilderExtensions
  {

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using var services = app.ApplicationServices.CreateScope();

      var dbContext = services.ServiceProvider.GetService<ApplicationDBContext>();

      dbContext.Database.Migrate();

    }

  }
}
