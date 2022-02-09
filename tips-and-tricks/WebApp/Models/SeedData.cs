using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) // Is the interface used to register middleware components. It also provides access to application's service including the EF Core Db context service.
        {
            var context = app.ApplicationServices
                             .CreateScope()
                             .ServiceProvider
                             .GetRequiredService<AppDbContext>();

            //context.Database.EnsureCreated(); // This will create the database. But if you already have migrations that are going to create this, then don't use this line.

            if (context.Database.GetPendingMigrations().Any()) // 20201116005345_Initial.cs created by running "dotnet ef migrations add Initial"
            {
                context.Database.Migrate(); //Keeps db schema and application model classes in sync
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price = 48.95m
                    }
                );
            }

            if (!context.Logs.Any())
            {
                context.Logs.AddRange(
                    new Log
                    {
                        Level = "ERROR",
                        Message = "Seed Message",
                        TimeStamp = DateTime.Now
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
