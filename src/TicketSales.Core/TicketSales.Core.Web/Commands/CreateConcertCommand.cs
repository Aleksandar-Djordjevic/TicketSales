using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSales.Core.Web.Commands
{
    public class CreateConcertCommand
    {
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
    }
}
