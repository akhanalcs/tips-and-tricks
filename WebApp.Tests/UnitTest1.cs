using System;
using WebApp.Tests.TestData;
using Xunit;

namespace WebApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            try
            {
                var products = Repository.GetProductsFromJSON();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
