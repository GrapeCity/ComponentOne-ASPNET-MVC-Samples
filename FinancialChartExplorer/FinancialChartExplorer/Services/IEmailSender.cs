using System.Threading.Tasks;

namespace FinancialChartExplorer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
