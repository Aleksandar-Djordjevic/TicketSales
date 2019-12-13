using System;

namespace TicketSales.Core.Web.Commands
{
    public class CreateConcertCommand
    {
        public string CommandId { get; }
        public string Name { get; }
        public int SeatingCapacity { get; }

        public CreateConcertCommand(string commandId, string name, int seatingCapacity)
        {
            CommandId = commandId;
            Name = name;
            SeatingCapacity = seatingCapacity;
        }
    }
}
