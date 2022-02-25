using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Tests.TestData
{
    internal static class Repository
    {
        public static IEnumerable<Product> GetProductsFromJSON()
        {
            var jsonFilePath = GetTestDataPath("Products.json");
            var json = System.IO.File.ReadAllText(jsonFilePath);

            var products = JsonSerializer.Deserialize<List<Product>>(json);
            return products;
        }

        public static string GetTestDataPath(string testDataFileName)
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            var pos = pathItems.Reverse().ToList().FindIndex(x => string.Equals("bin", x));
            string projectPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - pos - 1));
            return $"{projectPath}\\TestData\\{testDataFileName}";
        }
    }
}
