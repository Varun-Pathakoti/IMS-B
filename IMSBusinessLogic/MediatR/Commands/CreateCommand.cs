using IMSDomain.Entities;
using MediatR;

namespace IMSBusinessLogic.MediatR.Commands
{
    public class CreateCommand : IRequest<Product>
    {
        public string Description { get; set; }
        public string ProductName { get; set; }
        public int StockLevel { get; set; }
        public float Price { get; set; }
        public int Threshold { get; set; }
        public CreateCommand(string Name, string desc, float price, int thresh, int stock)
        {
            this.ProductName = Name;
            this.Description = desc;
            this.Price = price;
            this.Threshold = thresh;
            this.StockLevel = stock;

        }
    }
}
