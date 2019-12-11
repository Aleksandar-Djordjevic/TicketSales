using MassTransit;
using System.Threading.Tasks;
using TicketSales.Core.Application.UseCases.CreateConcert;
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
