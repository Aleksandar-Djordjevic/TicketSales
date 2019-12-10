namespace Domain.Models
{
    public class Tickets
    {
        public Concert Concert { get; }
        public TicketQuantity Quantity { get; }

        public Tickets(Concert concert, TicketQuantity quantity)
        {
            Concert = concert;
            Quantity = quantity;
        }
    }
}