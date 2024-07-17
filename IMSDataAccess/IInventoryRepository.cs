using IMSDomain.DTO;
using IMSDomain.Entities;

namespace IMSDataAccess
{
    public interface IInventoryRepository
    {
        Task<List<Product>> getAll();
        Task<List<Sale>> getAllSale();

        Task<Product> getbyId(int id);
        Task<Product> getByName(String name);
        Task deleteById(int id);
        Task<Product> create(Product product);
        Task<Product> update(int id, int stock);
        Task<List<int>> RecordSales(List<Order> sales);
        Task<string> GenerateReport();
        Task<Product> UpdateProduct(int id , UpdateProductDTO product);
    }
}
