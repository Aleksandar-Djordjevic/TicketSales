namespace TicketSales.Core.Domain.Events
{
    public class PurchaseFailedEvent
    {
        public string Id { get; set; }
        public string ConcertId { get; set; }
        public string ConcertName { get; set; }
        public string BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}
