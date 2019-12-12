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
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.MessageId.ToString()))
            {
                return;
            }

            var concert = await _concertsStore.Get(context.Message.Id);
            await concert.Match(
                Some: null, 
                None: () => _concertsStore.AddConcert(
                    new ConcertToBuy
                    {
                        MessageId = context.MessageId.ToString(),
                        Id = context.Message.Id,
                        Name = context.Message.Name,
                    })
            );
        }
    }
}
