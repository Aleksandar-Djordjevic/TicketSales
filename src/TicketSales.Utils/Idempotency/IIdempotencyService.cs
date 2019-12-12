using System.Threading.Tasks;

namespace TicketSales.Utils.Idempotency
{
    public interface IIdempotencyService
    {
        Task<bool> IsMessageAlreadyProcessed(string messageId);
    }
}