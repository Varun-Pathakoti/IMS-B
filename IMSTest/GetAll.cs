using IMSDataAccess;
using IMSDomain;
using IMSDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSTest
{
    [TestClass]
    public class GetAll
    {
        [TestMethod]
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        [TestMethod]
        public async Task GetAll_ReturnsAllProducts()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);
            context.Products.Add(new Product { ProductID = 1, ProductName = "TestProduct1", Description = "TestDescription1", StockLevel = 10, Price = 100, Threshold = 5 });
            context.Products.Add(new Product { ProductID = 2, ProductName = "TestProduct2", Description = "TestDescription2", StockLevel = 20, Price = 200, Threshold = 10 });
            context.SaveChanges();

            var inventory = new InventoryRepository(context);

            // Act
            var result = await inventory.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

    }
}
