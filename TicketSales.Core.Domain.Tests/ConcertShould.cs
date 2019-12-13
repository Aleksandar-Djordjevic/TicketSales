using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain.Models;
using Xunit;

namespace TicketSales.Core.Domain.Tests
{
    public class ConcertShould
    {
        [Fact]
        public void IssueTicketsWhenThereAreEnoughOfThem()
        {
            var concert = new Concert("Test concert", new TicketQuantity(10));
            var wantedQuantity = new TicketQuantity(5);

            var tickets = concert.IssueTickets(wantedQuantity);

            Assert.True(tickets.IsSuccess);
            Assert.Equal(5, tickets.Value.Quantity.Value);
        }

        [Fact]
        public void UpdateTicketsSoldWhenIssuingTickets()
        {
            var concert = new Concert("Test concert", new TicketQuantity(10));
            var firstWantedQuantity = new TicketQuantity(5);
            var secondWantedQuantity = new TicketQuantity(3);

            concert.IssueTickets(firstWantedQuantity);
            concert.IssueTickets(secondWantedQuantity);

            Assert.Equal(8, concert.TicketsSold);
        }

        [Fact]
        public void FailToIssueTicketsWhenThereAreNotEnoughOfThem()
        {
            var concert = new Concert("Test concert", new TicketQuantity(3));
            var wantedQuantity = new TicketQuantity(5);

            var tickets = concert.IssueTickets(wantedQuantity);

            Assert.True(tickets.IsFailure);
            Assert.Equal(Errors.NoEnoughTickets, tickets.Error);
        }
    }
}
