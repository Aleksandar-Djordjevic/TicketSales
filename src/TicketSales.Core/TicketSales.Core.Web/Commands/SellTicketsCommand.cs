using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSales.Core.Web.Commands
{
    public class SellTicketsCommand
    {
        public string ConcertId { get; set; }
        public string BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}
