﻿using CSharpFunctionalExtensions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Admin.Consumers
{
    public class ConcertCreatedEventHandler : IConsumer<ConcertCreatedEvent>
    {
        private readonly IStoreConcerts _concertsStore;

        public ConcertCreatedEventHandler(IStoreConcerts concertsStore)
        {
            _concertsStore = concertsStore;
        }

        public Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            _concertsStore.Get(context.Message.Id)
                .Match(null, () => _concertsStore.AddConcert(
                    new Concert
                    {
                        Id = context.Message.Id,
                        Name = context.Message.Name,
                        Capacity = context.Message.SeatingCapacity,
                    })
                );

            return Task.CompletedTask;
        }
    }
}
