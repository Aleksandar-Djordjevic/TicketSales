using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Application.UseCases.BuyTickets
{
    public class SellTicketsRequest
    {
        public string ConcertId { get; set; }
        public string BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}
