namespace TicketSales.Core.Domain.Events
{
    public class PurchaseSuccessfullyMadeEvent
    {
        public string ConcertId { get; set; }
        public string BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}