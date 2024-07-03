namespace IMSBusinessLogic
{
    public interface IEmail
    {
        Task<object> SendLowStockEmail(string ToEmail, string subject, string msg);
        Task<object> SendReportEmail(string ToEmail, string subject, string msg);
    }
}
