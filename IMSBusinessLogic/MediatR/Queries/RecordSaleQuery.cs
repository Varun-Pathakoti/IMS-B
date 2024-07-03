using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class RecordSaleQuery:IRequest<Product>
    {
        public int productid {  get; set; }
        public int quantity { get; set; }
        public RecordSaleQuery(int prodid,int quantity) 
        {
            this.productid = prodid;    
            this.quantity = quantity;
        }
    }
}
