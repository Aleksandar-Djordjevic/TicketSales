namespace TicketSales.Core.Application.UseCases.SellTickets
{
    public class SellTicketsRequest
    {
        public string ConcertId { get; set; }
        public string BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}
