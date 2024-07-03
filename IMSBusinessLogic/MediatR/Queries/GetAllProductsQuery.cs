using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class GetAllProductsQuery:IRequest<List<Product>>
    {
    }
}
