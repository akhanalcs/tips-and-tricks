using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Scheduling;
using WebApp.Scheduling.Tasks;
using WebApp.Services;

namespace WebApp
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
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });

            //I got help from here: https://stackoverflow.com/a/40838891/8644294
            //run 'dotnet ef database update' to create this database.
            // OR context.Database.EnsureCreated() in the SeedData class takes care of this.
            //services.AddDbContextPool<AppDbContext>(options => // Originally it was: AddDbContextPool. The pool caches the instance of AppDbContext, so it doesn't have to create everytime
            //{
            //    options.UseSqlite(Configuration.GetConnectionString("WebAppConnection")); 
            //});

            //services.AddDbContext<AppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("WebAppConnection")), ServiceLifetime.Transient, ServiceLifetime.Singleton);
            // AddScoped because we want Instance of EFStoreRepository to be alive through the entire scope of the HttpRequest.
            // New instance is created for every Http request, and lives theoughout the scope of that HttpRequest.
            //services.AddScoped<IAppRepository, EFAppRepository>(); // Letting know that EFAppRepository implements IAppRepository.
            services.AddSingleton<IAppRepository, InMemoryAppRepository>();

            services.AddSingleton<ILogPusher, LogPusher>();

            // Add scheduled tasks & scheduler
            services.AddSingleton<IScheduledTask, QuoteOfTheDayTask>();
            services.AddSingleton<IScheduledTask, ClearLogsTask>();
            services.AddScheduler((sender, args) =>
            {
                Console.Write(args.Exception.Message);
                args.SetObserved();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LogPusher>("/realTimeLogs");
                endpoints.MapRazorPages();
            });

            //SeedData.EnsurePopulated(app);
        }
    }
}
