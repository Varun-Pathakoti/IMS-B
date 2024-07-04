using IMSDomain;

namespace IMSDataAccess
{
    public interface IInventory
    {
        Task<List<Product>> getAll();

        Task<Product> getbyId(int id);
        Task<Product> create(Product product);
        Task<Product> update(int id, int stock);
        Task<List<Product>> RecordSales(List<Order> sales);
        Task<string> GenerateReport();
    }
}
