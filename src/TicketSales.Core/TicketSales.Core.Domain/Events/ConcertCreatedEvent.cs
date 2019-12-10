namespace TicketSales.Core.Domain.Events
{
    public class ConcertCreatedEvent
    {
        public string Name { get; }
        public int SeatingCapacity { get; }

        public ConcertCreatedEvent(string name, int seatingCapacity)
        {
            Name = name;
            SeatingCapacity = seatingCapacity;
        }
    }
}
