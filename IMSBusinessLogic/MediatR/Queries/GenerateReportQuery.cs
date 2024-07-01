using IMSDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class GenerateReportQuery : IRequest<Report>
    {
    }    
}
