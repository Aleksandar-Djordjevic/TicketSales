using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Core.Domain.Events;
using TicketSales.User.Models;
using TicketSales.User.Services;
using TicketSales.Utils.Idempotency;

namespace TicketSales.User.Consumers
{
    public class PurchaseSuccessfullyMadeEventHandler : IConsumer<PurchaseSuccessfullyMadeEvent>
    {
        private readonly IStorePurchases _purchaseStore;
        private readonly IIdempotencyService _idempotencyService;

        public PurchaseSuccessfullyMadeEventHandler(IStorePurchases purchaseStore, IIdempotencyService idempotencyService)
        {
            _purchaseStore = purchaseStore;
            _idempotencyService = idempotencyService;
        }

        public async Task Consume(ConsumeContext<PurchaseSuccessfullyMadeEvent> context)
        {
            if (await _idempotencyService.IsMessageAlreadyProcessed(context.Message.EventId))
            {
                return;
            }

            var purchase = await _purchaseStore.Get(context.Message.PurchaseId);
            await purchase.Match(
                Some: null,
                None: () => _purchaseStore.Add(
                    new Purchase
                    {
                        Id = context.Message.PurchaseId,
                        BuyerId = context.Message.BuyerId,
                        ConcertId = context.Message.ConcertId,
                        ConcertName = context.Message.ConcertName,
                        Quantity = context.Message.Quantity,
                        Status = "Successful"
                    })
            );            
        }
    }
}
