using IMSDataAccess;
using IMSDataAccess.Exceptions;
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
    public class UpdateStockTest
    {
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
        }
        [TestMethod]
        public async Task Update_UpdatesProductStockLevel()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);
            context.Products.Add(new Product { ProductID = 1, ProductName = "TestProduct", Description = "TestDescription", StockLevel = 10, Price = 100, Threshold = 5 });
            context.SaveChanges();

            var inventory = new InventoryRepository(context);

            // Act
            var result = await inventory.update(1, 20);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.StockLevel);
        }

        [TestMethod]
        
        public async Task Update_ReturnsNull_WhenProductDoesNotExist()
        {
            
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);

            var inventory = new InventoryRepository(context);

            await Assert.ThrowsExceptionAsync<ProductNotFoundException>(async () =>
            {
                await inventory.getbyId(1);
            });

        }

    }
    
}
