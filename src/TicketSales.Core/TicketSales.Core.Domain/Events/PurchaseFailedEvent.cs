using System;

namespace TicketSales.Core.Domain.Events
{
    public class PurchaseFailedEvent
    {
        public string EventId { get; }
        public string PurchaseId { get; }
        public string ConcertId { get; }
        public string ConcertName { get; }
        public string BuyerId { get; }
        public int Quantity { get; }

        public PurchaseFailedEvent(string eventId, string purchaseId, string concertId, string concertName, string buyerId, int quantity)
        {
            EventId = eventId;
            PurchaseId = purchaseId;
            ConcertId = concertId;
            ConcertName = concertName;
            BuyerId = buyerId;
            Quantity = quantity;
        }
    }
}
