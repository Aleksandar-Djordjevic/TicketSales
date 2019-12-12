using System.Threading.Tasks;
using MassTransit;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Core.Adapters
{
    public class EventsPublisher : IPublishEvents
    {
        private readonly IBus _bus;

        public EventsPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish(ConcertCreatedEvent @event)
        {
            await _bus.Publish(@event);
        }

        public async Task Publish(PurchaseSuccessfullyMadeEvent @event)
        {
            await _bus.Publish(@event);
        }

        public async Task Publish(PurchaseFailedEvent @event)
        {
            await _bus.Publish(@event);
        }
    }
}
