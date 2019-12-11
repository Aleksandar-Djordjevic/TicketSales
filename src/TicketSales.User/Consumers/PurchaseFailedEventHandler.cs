﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MassTransit;
using TicketSales.Core.Domain.Events;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Consumers
{
    public class PurchaseFailedEventHandler : IConsumer<PurchaseFailedEvent>
    {
        private readonly IStorePurchases _purchaseStore;

        public PurchaseFailedEventHandler(IStorePurchases purchaseStore)
        {
            _purchaseStore = purchaseStore;
        }

        public async Task Consume(ConsumeContext<PurchaseFailedEvent> context)
        {
            var purchase = await _purchaseStore.Get(context.Message.Id);
            await purchase.Match(
                Some: null,
                None: () => _purchaseStore.Add(
                    new Purchase
                    {
                        Id = context.Message.Id,
                        BuyerId = context.Message.BuyerId,
                        ConcertId = context.Message.ConcertId,
                        ConcertName = context.Message.ConcertName,
                        Quantity = context.Message.Quantity,
                        Status = "Failed"
                    })
            );
        }
    }
}
