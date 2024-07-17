using IMSDataAccess.Exceptions;
using IMSDomain.DTO;
using IMSDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
namespace IMSDataAccess
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ProductDbContext _db;
        

        public InventoryRepository(ProductDbContext db)
        {
            this._db = db;
        }



        public async Task<Product> getbyId(int id)
        {
            var product= await _db.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            if(product == null)
            {
                throw new ProductNotFoundException();
            }
            return product;
        }
        public async Task<List<Sale>> getAllSale()
        {
            var sales=await _db.Sales.ToListAsync();
            return sales;
        }

        public async Task<List<Product>> getAll()
        {
            //var prd = await getbyId(1);
            var product = await _db.Products.ToListAsync();//asnotracking
            return product;
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
                throw new NegativeNumerException("Value cannot be less than zero");
            }


            var product = await getbyId(id);
             
            product.StockLevel = stock;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;

        }


        public async Task<List<int>> RecordSales(List<Order> sales)
        {
            var updatedProducts = new List<Product>();
            var outOfStockProducts = new List<int>();
            foreach (var p in sales)
            {

                var product = await _db.Products.FindAsync(p.prodId);
                if (product.StockLevel < p.quantity)
                {
                    outOfStockProducts.Add(product.ProductID);
                    //Email email = new Email();
                    //await email.Emailmet("dasasaipooja@gmail.com", "out of stock", product.ProductName);
                }
                else
                {

                    product.StockLevel -= p.quantity;
                    updatedProducts.Add(product);

                    if (product.StockLevel < product.Threshold)
                    {
                        Email email = new Email();
                        await email.Emailmet("dasasaipooja@gmail.com", "Low Stock Alert", product.ProductName);

                    }

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
                    //else
                    //{
                    //    return (null, outOfStockProducts);
                    //}
                }
            }
            return outOfStockProducts;

        }

        public async Task deleteById(int id)
        {
            var product = await getbyId(id);

            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public async Task<Product> getByName(String name)
        {
            var product = _db.Products.FirstOrDefault(s=>s.ProductName == name);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            return product;
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
        
        public async Task<Product> UpdateProduct(int id, UpdateProductDTO product)
        {
            var _product = await getbyId(id);
            if(product.Description != null && product.Description.Length!=0) 
            {
                _product.Description = product.Description;
            }
            if(product.ProductName != null && product.ProductName.Length!=0)
            {
                _product.ProductName = product.ProductName;
            }
            if(product.Price != null &&product.Price>0)
            {
                _product.Price = product.Price;
            }
            if (product.Threshold != null && product.Threshold > 0)
            {
                _product.Threshold = product.Threshold;
            }
            if (_product.StockLevel != null && product.StockLevel > 0)
            {
                _product.StockLevel = product.StockLevel;
            }
            _db.Products.Update(_product);
            await _db.SaveChangesAsync();
            return _product;
        }
    }
 }
