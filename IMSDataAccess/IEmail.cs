using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDataAccess
{
    public interface IEmail
    {
        Task<object> Emailmet(string ToEmail, string subject, string msg);
        //Task<object> SendReportEmail(string ToEmail, string subject, string msg);
    }
}
