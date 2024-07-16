using IMSDataAccess;
using IMSDataAccess.Exceptions;
using IMSDomain;
using Microsoft.EntityFrameworkCore;

namespace IMSTest
{
    [TestClass]
    public class getByIdTest
    {
        [TestMethod]
        private DbContextOptions<ProductDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        

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
            Assert.AreEqual("TestProduct", result.ProductName);
        }

        [TestMethod]
        public async Task GetById_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using var context = new ProductDbContext(options);

            var inventory = new InventoryRepository(context);

            // Act
            //var result = await inventory.getbyId(1);

            // Assert
            await Assert.ThrowsExceptionAsync<ProductNotFoundException>(async()=>
            {
                await inventory.getbyId(1);
            });
        }
    }
}

