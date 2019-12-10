using Application.CreateConcert;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Core.Web.Commands;

namespace TicketSales.Core.Web.CommandHandlers
{
    public class CreateConcertCommandHandler : IConsumer<CreateConcertCommand>
    {
        private readonly ICreateConcertService _createConcertService;

        public CreateConcertCommandHandler(ICreateConcertService createConcertService)
        {
            _createConcertService = createConcertService;
        }

        public async Task Consume(ConsumeContext<CreateConcertCommand> context)
        {
            await _createConcertService.CreateConcert(
                new CreateConcertRequest {
                    Name = context.Message.Name,
                    SeatingCapacity = context.Message.SeatingCapacity
                });
        }
    }
}
