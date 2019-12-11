namespace TicketSales.User.Models
{
    public class Purchase
    {
        public string Id { get; set; }
        public string BuyerId { get; set; }
        public string ConcertId { get; set; }
        public string ConcertName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
