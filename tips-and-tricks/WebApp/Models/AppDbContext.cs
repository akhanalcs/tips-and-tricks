using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // send DbContextOptions to base from constructor. Dbcontext provides informaion such as connection string, db provider etc.

        public DbSet<Product> Products { get; set; } // To access the Db table
    }
}
