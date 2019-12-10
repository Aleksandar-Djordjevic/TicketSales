using TicketSales.Core.Domain.Events;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IPublishEvents
    {
        Task Publish(ConcertCreatedEvent concertCreatedEvent);
        Task Publish(PurchaseSuccessfullyMadeEvent purchaseSuccessfullyMadeEvent);
        Task Publish(PurchaseFailedEvent purchaseFailedEvent);
    }
}
