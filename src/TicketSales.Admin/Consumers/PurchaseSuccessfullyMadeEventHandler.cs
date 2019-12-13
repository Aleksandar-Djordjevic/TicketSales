using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Core.Domain.Events;
using TicketSales.Utils.Idempotency;

namespace TicketSales.Admin.Consumers
{
    public class PurchaseSuccessfullyMadeEventHandler : IConsumer<PurchaseSuccessfullyMadeEvent>
    {
        private readonly IStoreConcerts _concertsStore;
        private readonly IIdempotencyService _idempotencyService;

        public PurchaseSuccessfullyMadeEventHandler(IStoreConcerts concertsStore, IIdempotencyService idempotencyService)
        {
            _concertsStore = concertsStore;
            _idempotencyService = idempotencyService;
        }

        public async Task Consume(ConsumeContext<PurchaseSuccessfullyMadeEvent> context)
        {
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.Message.EventId))
            {
                return;
            }

            var maybeConcert = await _concertsStore.Get(context.Message.ConcertId);
            await maybeConcert.Match(
                Some: concert => UpdateConcert(concert, context.Message.Quantity), 
                None: () => LogUnknownConcert(context.Message.ConcertId));
        }

        private async Task UpdateConcert(Concert concert, int soldQuantity)
        {
            concert.TicketsSold += soldQuantity;
            await _concertsStore.UpdateConcert(concert);
        }

        private Task LogUnknownConcert(string messageConcertId)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
