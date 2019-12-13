using System;

namespace TicketSales.Core.Domain.Events
{
    public class ConcertCreatedEvent
    {
        public string EventId { get; }
        public string ConcertId { get; }
        public string Name { get; }
        public int SeatingCapacity { get; }

        public ConcertCreatedEvent(string eventId, string concertId, string name, int seatingCapacity)
        {
            EventId = eventId;
            ConcertId = concertId;
            Name = name;
            SeatingCapacity = seatingCapacity;
        }
    }
}
