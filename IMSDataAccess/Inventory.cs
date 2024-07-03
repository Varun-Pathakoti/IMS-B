using IMSDomain;
using Microsoft.EntityFrameworkCore;
namespace IMSDataAccess
{
    public class Inventory : IInventory
    {
        private readonly ProductDbContext _db;


        public Inventory(ProductDbContext db)
        {
            this._db = db;
        }



        public async Task<Product> getbyId(int id)
        {
            return await _db.Products.SingleOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<List<Product>> getAll()
        {
            var pro = await _db.Products.ToListAsync();
            return pro;
        }
        public async Task<Product> create(Product product)
        {
            Product p = new Product();
            p.Description = product.Description;
            p.ProductName = product.ProductName;
            // p.ProductID = product.ProductID;
            p.StockLevel = product.StockLevel;
            p.Price = product.Price;
            p.Threshold = product.Threshold;
            _db.Add(p);
            await _db.SaveChangesAsync();
            return product;
        }
        public async Task<Product> update(int id, int stock)
        {
            var pro = await _db.Products.FindAsync(id);
            if (pro != null)
            {
                pro.StockLevel = stock;
                _db.Products.Update(pro);
                await _db.SaveChangesAsync();
                return pro;

            }
            else
            {
                return null;
            }
        }
        public async Task<Product> RecordSale(int productId, int quantity)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product != null)
            {

                product.StockLevel -= quantity;

                var sale = await _db.Sales.FirstOrDefaultAsync(s => s.ProductId == productId);
                if (sale != null)
                {
                    //sale.ProductId = productId;
                    sale.Quantity += quantity;
                    sale.SaleDate = DateTime.UtcNow;
                    //sale.ProductName = product.ProductName;
                    _db.Sales.Update(sale);

                }
                else
                {
                    var sale1 = new Sale
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        SaleDate = DateTime.UtcNow,
                        ProductName = product.ProductName
                    };

                    _db.Sales.Add(sale1);
                }
                _db.Products.Update(product);

                await _db.SaveChangesAsync();

            }
            else
            {
                return null;
            }
            return product;

        }

        public async Task<Report> GenerateReport()
        {
            // Fetch all products and sales asynchronously
            var products = await _db.Products.ToListAsync();
            var sales = await _db.Sales.ToListAsync();

            // Identify fast-moving product IDs (top 5 by quantity sold)
            var fastMovingProductIds = sales
                .OrderByDescending(s => s.Quantity)
                .Take(5)
                .Select(s => s.ProductId)
                .ToList();

            // Filter products for fast-moving products
            var fastMovingProducts = products
    .Where(p => fastMovingProductIds.Contains(p.ProductID))
    .Select(p => p.ProductName)
    .ToList();

            // Identify slow-moving product IDs (bottom 5 by quantity sold)
            var slowMovingProductIds = sales
                .OrderBy(s => s.Quantity)
                .Take(5)
                .Select(s => s.ProductId)
                .ToList();

            // Filter products for slow-moving products
            var slowMovingProducts = products
                .Where(p => slowMovingProductIds.Contains(p.ProductID)).Select(p => p.ProductName)
                .ToList();

            // Return the results as specified in the method signature
            Report r = new Report();
            r.Product = products;
            r.FastMovingProducts = fastMovingProducts;
            r.SlowMovingProducts = slowMovingProducts;
            return r;
        }
    }
 }
