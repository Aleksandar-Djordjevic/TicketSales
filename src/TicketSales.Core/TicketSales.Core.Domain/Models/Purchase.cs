namespace Domain.Models
{
    public class Purchase
    {
        public TicketsBuyer Buyer { get; }
        public Tickets Tickets { get; }

        public Purchase(TicketsBuyer buyer, Tickets tickets)
        {
            Buyer = buyer;
            Tickets = tickets;
        }
    }
}