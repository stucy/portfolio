using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_server.Models
{
    public class CommentsDBContext:DbContext
    {

        public CommentsDBContext(DbContextOptions<CommentsDBContext> options):base(options)
        {

        }

        public DbSet<CommentsModel> Comments { get; set; }

    }
}
