using MassTransit;
using System.Threading.Tasks;
using TicketSales.Core.Application.UseCases.CreateConcert;
using TicketSales.Core.Web.Commands;
using TicketSales.Utils.Idempotency;

namespace TicketSales.Core.Web.CommandHandlers
{
    public class CreateConcertCommandHandler : IConsumer<CreateConcertCommand>
    {
        private readonly ICreateConcertService _createConcertService;
        private readonly IIdempotencyService _idempotencyService;

        public CreateConcertCommandHandler(ICreateConcertService createConcertService, IIdempotencyService idempotencyService)
        {
            _createConcertService = createConcertService;
            _idempotencyService = idempotencyService;
        }

        public async Task Consume(ConsumeContext<CreateConcertCommand> context)
        {
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.Message.CommandId))
            {
                return;
            }

            await _createConcertService.CreateConcert(
                new CreateConcertRequest {
                    Name = context.Message.Name,
                    SeatingCapacity = context.Message.SeatingCapacity
                });
        }
    }
}
