using IMSDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic
{
    public interface IEmail
    {
        Task<object> SendLowStockEmail(string ToEmail, string subject, string msg);
        Task<object> SendReportEmail(string ToEmail, string subject, string msg);
    }
}
