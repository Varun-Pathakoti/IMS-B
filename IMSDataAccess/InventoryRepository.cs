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



        public async Task<Product> GetById(int id)
        {
            var product= await _db.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            if(product == null)
            {
                throw new ProductNotFoundException();
            }
            return product;
        }
        public async Task<List<Sale>> GetAllSale()
        {
            var sales=await _db.Sales.ToListAsync();
            return sales;
        }

        public async Task<List<Product>> GetAll()
        { 
            var product = await _db.Products.ToListAsync(); //asnotracking
            return product;
        }
        public async Task<Product> Create(Product product)
        {
            Product _product = new Product();
            _product.Description = product.Description;
            _product.ProductName = product.ProductName;
            // p.ProductID = product.ProductID;
            _product.StockLevel = product.StockLevel;
            _product.Price = product.Price;
            _product.Threshold = product.Threshold;
            _db.Add(_product);
            await _db.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Update(int id, int stock)
        {
            if (stock < 0)
            {
                throw new NegativeNumerException("Value cannot be less than zero");
            }


            var product = await GetById(id);
             
            product.StockLevel = stock;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;

        }


        public async Task<List<int>> RecordSales(List<Order> sales)
        {
            var updatedProducts = new List<Product>();
            var outOfStockProducts = new List<int>();
            foreach (var orderedProduct in sales)
            {

                var product = await _db.Products.FindAsync(orderedProduct.prodId);
                if (product.StockLevel < orderedProduct.quantity)
                {
                    outOfStockProducts.Add(product.ProductID);
                    
                }
                else
                {

                    product.StockLevel -= orderedProduct.quantity;
                    updatedProducts.Add(product);

                    if (product.StockLevel < product.Threshold)
                    {
                        Email email = new Email();
                        await email.Emailmet("dasasaipooja@gmail.com", "Low Stock Alert", product.ProductName);

                    }
                    await AddInSale(product, orderedProduct);
                    
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

        public async Task DeleteById(int id)
        {
            var product = await GetById(id);

            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public async Task<List<Product>> GetByName(String name)
        {
            //var product = _db.Products.FirstOrDefault(s=>s.ProductName == name);
            var product = _db.Products.Where(p => p.ProductName.Contains(name)).ToList();
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
            var _product = await GetById(id);
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

        public async Task AddInSale(Product product,Order orderedProduct)
        {
            var sale = await _db.Sales.FirstOrDefaultAsync(s => s.ProductId == orderedProduct.prodId);
            if (sale != null)
            {

                sale.Quantity += orderedProduct.quantity;
                sale.SaleDate = DateTime.UtcNow;

                _db.Sales.Update(sale);

            }
            else
            {
                var sale1 = new Sale
                {
                    ProductId = orderedProduct.prodId,
                    Quantity = orderedProduct.quantity,
                    SaleDate = DateTime.UtcNow,
                    ProductName = product.ProductName
                };

                _db.Sales.Add(sale1);
            }
            await _db.SaveChangesAsync();
        }

    }
 }
