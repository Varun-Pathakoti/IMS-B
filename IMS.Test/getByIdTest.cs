using IMSDataAccess;
using IMSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Test
{
    public class getByIdTest
    {
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        //private readonly IInventoryRepository _inventory;
        //getByIdTest(IInventoryRepository inventory) 
        //{
        //    this._inventory = inventory;
            
        //}
        

    [TestMethod]
        public async Task getById_ReturnsProduct_WhenProductExists()
        {
            
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);
            context.Products.Add(new Product { ProductID = 1, ProductName = "TestProduct", Description = "TestDescription", StockLevel = 10, Price = 100, Threshold = 5 });
            context.SaveChanges();
            var inventory = new InventoryRepository(context);
            // Act
            var result = await inventory.getbyId(1);


            // Assert
            Assert.IsNotNull(result);
            Assert.Equals("TestProduct", result.ProductName);
        }
    }
}
