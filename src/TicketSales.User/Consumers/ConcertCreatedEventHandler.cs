using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Core.Domain.Events;
using TicketSales.User.Models;
using TicketSales.User.Services;
using TicketSales.Utils.Idempotency;

namespace TicketSales.User.Consumers
{
    public class ConcertCreatedEventHandler : IConsumer<ConcertCreatedEvent>
    {
        private readonly IStoreConcerts _concertsStore;
        private readonly IIdempotencyService _idempotencyService;

        public ConcertCreatedEventHandler(IStoreConcerts concertsStore, IIdempotencyService idempotencyService)
        {
            _concertsStore = concertsStore;
            _idempotencyService = idempotencyService;
        }

        public async Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.Message.EventId))
            {
                return;
            }

            var concert = await _concertsStore.Get(context.Message.ConcertId);
            await concert.Match(
                Some: null, 
                None: () => _concertsStore.AddConcert(
                    new ConcertToBuy
                    {
                        Id = context.Message.ConcertId,
                        Name = context.Message.Name,
                    })
            );
        }
    }
}
