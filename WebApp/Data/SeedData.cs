using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

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

            //if (!context.Logs.Any())
            //{
            //    context.Logs.AddRange(
            //        new Log
            //        {
            //            Level = "ERROR",
            //            Message = "Seed Message",
            //            TimeStamp = DateTime.Now
            //        }
            //    );
            //}

            context.SaveChanges();
        }

        //public static void ReadSqliteDbTables()
        //{
        //    var connectionStringBuilder = new SqliteConnectionStringBuilder();
        //    connectionStringBuilder.DataSource = "../WebApp.db";

        //    using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        //    {
        //        connection.Open();
        //        var selectCmd = connection.CreateCommand();
        //        selectCmd.CommandText = "SELECT name FROM sqlite_schema WHERE type = 'table' ORDER BY name;";

        //        using (var reader = selectCmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var message = reader.GetString(0);
        //                Console.WriteLine(message);
        //            }
        //        }
        //    }
        //}
    }
}
