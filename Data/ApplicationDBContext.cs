using client_server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_server.Models
{
    public class ApplicationDBContext: IdentityDbContext<UsersModel>
  {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) {}

        public DbSet<CommentsModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UsersModel>()
              .Ignore(u => u.EmailConfirmed)
              .Ignore(u => u.AccessFailedCount)
              .Ignore(u => u.LockoutEnabled)
              .Ignore(u => u.SecurityStamp)
              .Ignore(u => u.ConcurrencyStamp)
              .Ignore(u => u.PhoneNumber)
              .Ignore(u => u.PhoneNumberConfirmed)
              .Ignore(u => u.TwoFactorEnabled)
              .Ignore(u => u.LockoutEnd)
              .Ignore(u => u.ConcurrencyStamp);

            base.OnModelCreating(builder);
        }

        }
}
