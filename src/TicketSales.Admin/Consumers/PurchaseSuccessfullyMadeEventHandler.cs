using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Admin.Consumers
{
    public class PurchaseSuccessfullyMadeEventHandler : IConsumer<PurchaseSuccessfullyMadeEvent>
    {
        private readonly IStoreConcerts _concertsStore;

        public PurchaseSuccessfullyMadeEventHandler(IStoreConcerts concertsStore)
        {
            _concertsStore = concertsStore;
        }

        public async Task Consume(ConsumeContext<PurchaseSuccessfullyMadeEvent> context)
        {
            var maybeConcert = await _concertsStore.Get(context.Message.ConcertId);
            await maybeConcert.Match(
                Some: concert => UpdateConcert(concert, context.Message.Quantity), 
                None: () => LogUnknownConcert(context.Message.ConcertId));
        }

        private async Task UpdateConcert(Concert concert, int soldQuantity)
        {
            concert.TicketsSold += soldQuantity;
            await _concertsStore.UpdateConcert(concert);
        }

        private Task LogUnknownConcert(string messageConcertId)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
