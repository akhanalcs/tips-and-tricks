using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext context;
        public AppRepository(AppDbContext ctx) // To talk to SQL server, This repository needs DbContext that's why it's injected here.
        {
            context = ctx;
        }

        public IQueryable<Log> Logs
        {
            get
            {
                return context.Logs;
            }
        }

        public void ClearLogs()
        {
            var itemsToDelete = context.Set<Log>();
            context.Logs.RemoveRange(itemsToDelete);
            context.SaveChanges();
        }
    }
}
