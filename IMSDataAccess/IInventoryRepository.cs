using IMSDomain.DTO;
using IMSDomain.Entities;

namespace IMSDataAccess
{
    public interface IInventoryRepository
    {
        Task<List<Product>> GetAll();
        Task<List<Sale>> GetAllSale();

        Task<Product> GetById(int id);
        Task<List<Product>> GetByName(String name);
        Task DeleteById(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(int id, int stock);
        Task<List<int>> RecordSales(List<Order> sales);
        Task<string> GenerateReport();
        Task<Product> UpdateProduct(int id , UpdateProductDTO product);
        Task AddInSale(Product product, Order orderedProduct);
    }
}
