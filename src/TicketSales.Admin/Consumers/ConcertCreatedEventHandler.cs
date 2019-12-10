using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Admin.Consumers
{
    public class ConcertCreatedEventHandler : IConsumer<ConcertCreatedEvent>
    {
        public Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
