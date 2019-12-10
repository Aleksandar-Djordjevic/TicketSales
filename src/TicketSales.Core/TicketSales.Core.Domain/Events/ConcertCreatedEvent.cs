namespace TicketSales.Core.Domain.Events
{
    public class ConcertCreatedEvent
    {
        public string Id { get; }
        public string Name { get; }
        public int SeatingCapacity { get; }

        public ConcertCreatedEvent(string id, string name, int seatingCapacity)
        {
            Id = id;
            Name = name;
            SeatingCapacity = seatingCapacity;
        }
    }
}
