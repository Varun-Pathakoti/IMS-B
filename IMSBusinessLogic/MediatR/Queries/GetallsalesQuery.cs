using IMSDomain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class GetallSalesQuery:IRequest<List<Sale>>
    {
    }
}
