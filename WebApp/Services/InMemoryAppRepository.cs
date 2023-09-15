using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class InMemoryAppRepository : IAppRepository
    {
        public IQueryable<Product> Products
        {
            get
            {
                return (new Product[]
                {
                    new Product
                    {
                        ProductID = 1,
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275
                    },
                    new Product
                    {
                        ProductID = 2,
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price = 48.95m
                    }
                }).AsQueryable();
            }
        }

        public void CreateProduct(Product p)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product p)
        {
            throw new NotImplementedException();
        }

        public void SaveProduct(Product p)
        {
            throw new NotImplementedException();
        }

        private List<Log> _logs = new();
        public IQueryable<Log> Logs
        {
            get
            {
                return _logs.AsQueryable();
            }
        }

        public void CreateLog(Log l)
        {
            _logs.Add(l);
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }
    }
}
