﻿using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Models;
using TicketSales.Core.Domain.Services;

namespace TicketSales.Core.Application.UseCases.SellTickets
{
    public class SellTicketsService : ISellTicketsService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ITicketsService _ticketsService;
        private readonly IPublishEvents _eventPublisher;

        public SellTicketsService(
            IConcertRepository concertRepository, 
            IBuyerRepository buyerRepository, 
            ITicketsService ticketsService, 
            IPublishEvents eventPublisher)
        {
            _concertRepository = concertRepository;
            _buyerRepository = buyerRepository;
            _ticketsService = ticketsService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Result<Purchase>> SellTickets(SellTicketsRequest request)
        {
            var possibleConcert = await _concertRepository.GetConcert(request.ConcertId);
            var result = await possibleConcert
                .ToResult(Errors.ConcertNotFound)
                .Bind(async concert =>
                {
                    var possibleBuyer = await _buyerRepository.GetBuyer(request.BuyerId);
                    return possibleBuyer
                        .ToResult(Errors.BuyerNotFound)
                        .Bind(buyer => _ticketsService.SellTickets(concert, buyer, new TicketQuantity(request.Quantity)))
                        .Tap(purchase =>
                        {
                            _eventPublisher.Publish(new PurchaseSuccessfullyMadeEvent(
                                Guid.NewGuid().ToString(),
                                purchase.Id,
                                purchase.Tickets.Concert.Id,
                                purchase.Tickets.Concert.Name,
                                purchase.Buyer.Id,
                                purchase.Tickets.Quantity
                            ));
                        });
                })
                .OnFailure(error => {
                    _eventPublisher.Publish(new PurchaseFailedEvent(
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString(),
                        request.ConcertId, 
                        "Unknown", 
                        request.BuyerId, 
                        request.Quantity));
                });
            return result;
        }
    }
}
