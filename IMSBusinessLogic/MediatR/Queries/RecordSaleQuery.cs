using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class RecordSaleQuery : IRequest<List<int>>
    {
        public List<Order> sales { get; set; }

        public RecordSaleQuery(List<Order> sales)
        {
            this.sales = sales;
        }
    }
}
