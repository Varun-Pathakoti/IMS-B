using IMSDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDataAccess
{
    public interface IInventory
    {
        Task<List<Product>> getAll();

        Task<Product> getbyId(int id);
        Task<Product> create(Product product);
        Task<Product> update(int id, int stock);
        Task<Product> RecordSale(int ProductId, int Quantity);
        Task<Report> GenerateReport();
    }
}
