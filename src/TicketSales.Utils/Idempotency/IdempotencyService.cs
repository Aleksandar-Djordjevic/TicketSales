using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketSales.Utils.Idempotency
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly HashSet<string> _processedMessages = new HashSet<string>();

        public Task<bool> IsMessageAlreadyProcessed(string messageId)
        {
            return Task.FromResult(!_processedMessages.Add(messageId));
        }
    }
}
