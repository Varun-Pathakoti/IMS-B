using IMSDomain.Entities;
using MediatR;

namespace IMSBusinessLogic.MediatR.Commands
{
    public class UpdateCommand:IRequest<Product>
    {
        public int Id { get; set; }
        public int Stock {  get; set; }
        public UpdateCommand(int id,int stock)
        {
            this.Id = id;
            this.Stock = stock;

        }
    }
}
