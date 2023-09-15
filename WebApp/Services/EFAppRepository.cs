using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class EFAppRepository : IAppRepository
    {
        private AppDbContext context;
        public EFAppRepository(AppDbContext ctx) // To talk to SQL server, This repository needs DbContext that's why it's injected here.
        {
            context = ctx;
        }

        public IQueryable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }

        public IQueryable<Log> Logs
        {
            get
            {
                return context.Logs;
            }
        }

        public void CreateLog(Log l)
        {
            context.Add(l);
            context.SaveChanges();
        }

        public void ClearLogs()
        {
            var itemsToDelete = context.Set<Log>();
            context.Logs.RemoveRange(itemsToDelete);
            context.SaveChanges();
        }
    }
}
