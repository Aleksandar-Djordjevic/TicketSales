using System;

namespace TicketSales.Core.Web.Commands
{
    public class SellTicketsCommand
    {
        public string CommandId { get; }
        public string ConcertId { get; }
        public string BuyerId { get; }
        public int Quantity { get; }

        public SellTicketsCommand(string commandId, string concertId, string buyerId, int quantity)
        {
            CommandId = commandId;
            ConcertId = concertId;
            BuyerId = buyerId;
            Quantity = quantity;
        }
    }
}
