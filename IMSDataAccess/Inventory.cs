using IMSDomain;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
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
            if (stock < 0)
            {
                throw new Exception("Value cannot be less than zero");
            }
            
            
                var pro = await _db.Products.FindAsync(id);
                if (pro == null)
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

        public async Task<List<Product>> RecordSales(List<Order> sales)
        {
            var updatedProducts = new List<Product>();
            foreach (var p in sales)
            {
              
                var product = await _db.Products.FindAsync(p.prodId);
                if (product != null)
                {
                    if (product.StockLevel < p.quantity)
                    {

                        Email email = new Email();
                        await email.Emailmet("dasasaipooja@gmail.com", "out of stock", product.ProductName);
                    }
                    else
                    {

                        product.StockLevel -= p.quantity;
                        updatedProducts.Add(product);

                        var sale = await _db.Sales.FirstOrDefaultAsync(s => s.ProductId == p.prodId);
                        if (sale != null)
                        {

                            sale.Quantity += p.quantity;
                            sale.SaleDate = DateTime.UtcNow;

                            _db.Sales.Update(sale);

                        }
                        else
                        {
                            var sale1 = new Sale
                            {
                                ProductId = p.prodId,
                                Quantity = p.quantity,
                                SaleDate = DateTime.UtcNow,
                                ProductName = product.ProductName
                            };

                            _db.Sales.Add(sale1);
                        }
                        _db.Products.Update(product);

                        await _db.SaveChangesAsync();

                    }
                }
                else
                {
                    return null;
                }
            }
            return updatedProducts;

        }

        public async Task<string> GenerateReport()
        {
                    var products = await _db.Products.ToListAsync();
            var sales = await _db.Sales.ToListAsync();

            var fastMovingProductIds = sales
                .OrderByDescending(s => s.Quantity)
                .Take(5)
                .Select(s => s.ProductId)
                .ToList();            
            var fastMovingProducts = products
            .Where(p => fastMovingProductIds.Contains(p.ProductID))
            .Select(p => p.ProductName)
            .ToList();

            var slowMovingProductIds = sales
                .OrderBy(s => s.Quantity)
                .Take(5)
                .Select(s => s.ProductId)
                .ToList();

            var slowMovingProducts = products
                .Where(p => slowMovingProductIds.Contains(p.ProductID)).Select(p => p.ProductName)
                .ToList();
            var productsList = string.Join(", ", products.Select(p => $"{p.ProductName} (Stock: {p.StockLevel})"));
            var fastMovingList = string.Join(", ", fastMovingProducts);
            var slowMovingList = string.Join(", ", slowMovingProducts);

            var str = $"Products: {productsList}\nFast Moving Products: {fastMovingList}\nSlow Moving Products: {slowMovingList}";
            
            return str;
        }
    }
 }
