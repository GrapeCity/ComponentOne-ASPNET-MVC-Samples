using System.Threading.Tasks;

namespace FinancialChartExplorer.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
