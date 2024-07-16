using IMSDataAccess;
using IMSDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSTest
{
    [TestClass]
    public class AddProductTest
    {
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        [TestMethod]
        public async Task Create_AddsProductToDatabase()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);
            var inventory = new InventoryRepository(context);
            var newProduct = new Product { ProductName = "NewProduct", Description = "NewDescription", StockLevel = 15, Price = 150, Threshold = 7 };

            // Act
            var result = await inventory.create(newProduct);

            // Assert
            Assert.AreEqual(1, context.Products.Count());
            Assert.AreEqual("NewProduct", context.Products.First().ProductName);
        }
    }
}
