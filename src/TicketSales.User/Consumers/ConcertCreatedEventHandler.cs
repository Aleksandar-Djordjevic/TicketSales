using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Core.Domain.Events;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Consumers
{
    public class ConcertCreatedEventHandler : IConsumer<ConcertCreatedEvent>
    {
        private readonly IStoreConcerts _concertsStore;

        public ConcertCreatedEventHandler(IStoreConcerts concertsStore)
        {
            _concertsStore = concertsStore;
        }

        public async Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            var concert = await _concertsStore.Get(context.Message.Id);
            await concert.Match(
                Some: null, 
                None: () => _concertsStore.AddConcert(
                    new ConcertToBuy
                    {
                        Id = context.Message.Id,
                        Name = context.Message.Name,
                    })
            );
        }
    }
}
