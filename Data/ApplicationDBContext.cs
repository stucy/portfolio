using client_server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_server.Models
{
    public class ApplicationDBContext:DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) {}

        public DbSet<CommentsModel> Comments { get; set; }

        public DbSet<UsersModel> Users { get; set; }

    }
}
