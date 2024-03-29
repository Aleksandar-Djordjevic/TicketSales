﻿using CSharpFunctionalExtensions;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Domain.Services
{
    public interface ITicketsService
    {
        Result<Purchase> SellTickets(IConcert concert, TicketsBuyer buyer, TicketQuantity quantity);
    }
}