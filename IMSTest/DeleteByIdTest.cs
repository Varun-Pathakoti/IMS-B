using IMSDataAccess;
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
    public class DeleteByIdTest
    {
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        [TestMethod]
        public async Task DeletesProduct()
        {
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);
            var inventory = new InventoryRepository(context);
            context.Products.Add(new Product { ProductID = 1, ProductName = "TestProduct", Description = "TestDescription", StockLevel = 10, Price = 100, Threshold = 5 });
            context.SaveChanges();
            await inventory.DeleteById(1);

            Assert.AreEqual(0, context.Products.Count());
        }


    }
}

