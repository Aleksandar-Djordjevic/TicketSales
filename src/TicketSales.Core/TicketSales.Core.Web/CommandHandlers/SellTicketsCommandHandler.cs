using MassTransit;
using System.Threading.Tasks;
using TicketSales.Core.Application.UseCases.SellTickets;
using TicketSales.Core.Web.Commands;
using TicketSales.Utils.Idempotency;

namespace TicketSales.Core.Web.CommandHandlers
{
    public class SellTicketsCommandHandler : IConsumer<SellTicketsCommand>
    {
        private readonly ISellTicketsService _sellTicketsService;
        private readonly IIdempotencyService _idempotencyService;

        public SellTicketsCommandHandler(ISellTicketsService sellTicketsService, IIdempotencyService idempotencyService)
        {
            _sellTicketsService = sellTicketsService;
            _idempotencyService = idempotencyService;
        }

        public async Task Consume(ConsumeContext<SellTicketsCommand> context)
        {
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.MessageId.ToString()))
            {
                return;
            }

            await _sellTicketsService.SellTickets(
                new SellTicketsRequest {
                    ConcertId = context.Message.ConcertId,
                    BuyerId = context.Message.BuyerId,
                    Quantity = context.Message.Quantity
                });
        }
    }
}
