using System.Threading.Tasks;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Core.Application.Ports
{
    public interface IPublishEvents
    {
        Task Publish(ConcertCreatedEvent concertCreatedEvent);
        Task Publish(PurchaseSuccessfullyMadeEvent purchaseSuccessfullyMadeEvent);
        Task Publish(PurchaseFailedEvent purchaseFailedEvent);
    }
}
