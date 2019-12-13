using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Moq;
using TicketSales.Core.Domain.Models;
using TicketSales.Core.Domain.Services;
using Xunit;

namespace TicketSales.Core.Domain.Tests
{
    public class TicketServiceShould
    {
        [Fact]
        public void ReturnPurchaseWhenIssuingTicketsSucceeds()
        {
            var tickets = BuildTickets();
            var concert = SetUpConcertWhichIssueTickets(tickets);
            var buyer = new TicketsBuyer("1", "Buyer name");
            var wantedQuantity = new TicketQuantity(5);
            var service = new TicketsService();

            var purchase = service.SellTickets(concert, buyer, wantedQuantity);

            Assert.True(purchase.IsSuccess);
            Assert.Equal(buyer.Id, purchase.Value.Buyer.Id);
            Assert.Equal(tickets, purchase.Value.Tickets);
        }

        [Fact]
        public void ReturnFailureWhenIssuingTicketsFails()
        {
            var errorMessage = "Some error occured";
            var concert = SetUpConcertWhichDoesNotIssueTickets(errorMessage);
            var buyer = new TicketsBuyer("1", "Buyer name");
            var wantedQuantity = new TicketQuantity(5);
            var service = new TicketsService();

            var purchase = service.SellTickets(concert, buyer, wantedQuantity);

            Assert.True(purchase.IsFailure);
            Assert.Equal(errorMessage, purchase.Error);
        }

        private IConcert SetUpConcertWhichIssueTickets(Tickets tickets)
        {
            var concertMock = new Mock<IConcert>();
            concertMock
                .Setup(con => con.IssueTickets(It.IsAny<TicketQuantity>()))
                .Returns(Result.Success<Tickets>(tickets));
            return concertMock.Object;
        }

        private IConcert SetUpConcertWhichDoesNotIssueTickets(string errorMessage)
        {
            var concertMock = new Mock<IConcert>();
            concertMock
                .Setup(con => con.IssueTickets(It.IsAny<TicketQuantity>()))
                .Returns(Result.Failure<Tickets>(errorMessage));
            return concertMock.Object;
        }

        private Tickets BuildTickets()
        {
            return new Tickets(
                new Concert("Test concert", new TicketQuantity(10)),
                new TicketQuantity(2)
                );
        }
    }
}
