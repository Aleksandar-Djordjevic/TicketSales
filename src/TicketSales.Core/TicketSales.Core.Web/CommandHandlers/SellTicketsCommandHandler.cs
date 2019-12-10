﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Core.Application.UseCases.BuyTickets;
using TicketSales.Core.Web.Commands;

namespace TicketSales.Core.Web.CommandHandlers
{
    public class SellTicketsCommandHandler : IConsumer<SellTicketsCommand>
    {
        private readonly ISellTicketsService _sellTicketsService;

        public SellTicketsCommandHandler(ISellTicketsService sellTicketsService)
        {
            _sellTicketsService = sellTicketsService;
        }

        public async Task Consume(ConsumeContext<SellTicketsCommand> context)
        {
            await _sellTicketsService.SellTickets(
                new SellTicketsRequest {
                    ConcertId = context.Message.ConcertId,
                    BuyerId = context.Message.BuyerId,
                    Quantity = context.Message.Quantity
                });
        }
    }
}
