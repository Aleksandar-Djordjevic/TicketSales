using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Moq;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Application.UseCases.SellTickets;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Models;
using TicketSales.Core.Domain.Services;
using Xunit;

namespace TicketSales.Core.Application.Tests
{
    public class SellTicketsServiceShould
    {
        private readonly SellTicketsRequest _request = new SellTicketsRequest
        {
            BuyerId = "buyer1",
            ConcertId = "concert1",
            Quantity = 5
        };

        [Fact]
        public async Task ReturnFailureWhenConcertDoesNotExist()
        {
            var concertRepo = SetupConcertRepoWithNoConcert();
            var service = new SellTicketsService(
                concertRepo, 
                Mock.Of<IBuyerRepository>(),
                Mock.Of<ITicketsService>(), 
                Mock.Of<IPublishEvents>());

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsFailure);
            Assert.Equal(Errors.ConcertNotFound, purchaseResult.Error);
        }

        [Fact]
        public async Task ReturnFailureWhenBuyertDoesNotExist()
        {
            var concertRepo = SetupConcertRepoWithConcert();
            var buyerRepo = SetupBuyerRepoWithNoBuyer();
            var service = new SellTicketsService(
                concertRepo,
                buyerRepo,
                Mock.Of<ITicketsService>(),
                Mock.Of<IPublishEvents>());

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsFailure);
            Assert.Equal(Errors.BuyerNotFound, purchaseResult.Error);
        }

        [Fact]
        public async Task ReturnFailureWhenSellingTicketsFails()
        {
            var concertRepo = SetupConcertRepoWithConcert();
            var buyerRepo = SetupBuyerRepoWithBuyer();
            var error = "some error occured";
            var ticketService = SetupTicketsServiceWhichFailsWithError(error);
            var service = new SellTicketsService(
                concertRepo,
                buyerRepo,
                ticketService,
                Mock.Of<IPublishEvents>());

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsFailure);
            Assert.Equal(error, purchaseResult.Error);
        }

        [Fact]
        public async Task ReturnSuccessWhenSellingTicketsSucceeds()
        {
            var concertRepo = SetupConcertRepoWithConcert();
            var buyerRepo = SetupBuyerRepoWithBuyer();
            var purchase = BuildPurchase();
            var ticketService = SetupTicketsServiceWhichSucceedsWithPurchase(purchase);
            var service = new SellTicketsService(
                concertRepo,
                buyerRepo,
                ticketService,
                Mock.Of<IPublishEvents>());

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsSuccess);
            Assert.Equal(purchase, purchaseResult.Value);
        }

        [Fact]
        public async Task PublicSuccessWhenSellingTicketsSucceeds()
        {
            var concertRepo = SetupConcertRepoWithConcert();
            var buyerRepo = SetupBuyerRepoWithBuyer();
            var purchase = BuildPurchase();
            var ticketService = SetupTicketsServiceWhichSucceedsWithPurchase(purchase);
            var publisherMock = new Mock<IPublishEvents>();
            var service = new SellTicketsService(
                concertRepo,
                buyerRepo,
                ticketService,
                publisherMock.Object);

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsSuccess);
            publisherMock.Verify(publisher => publisher.Publish(It.IsAny<PurchaseSuccessfullyMadeEvent>()), Times.Once);
        }

        [Fact]
        public async Task PublishFailureWhenSellingTicketsFails()
        {
            var concertRepo = SetupConcertRepoWithNoConcert();
            var publisherMock = new Mock<IPublishEvents>();
            var service = new SellTicketsService(
                concertRepo,
                Mock.Of<IBuyerRepository>(),
                Mock.Of<ITicketsService>(),
                publisherMock.Object);

            var purchaseResult = await service.SellTickets(_request);

            Assert.True(purchaseResult.IsFailure);
            publisherMock.Verify(publisher => publisher.Publish(It.IsAny<PurchaseFailedEvent>()), Times.Once);
        }

        private IConcertRepository SetupConcertRepoWithNoConcert()
        {
            var concertRepo = new Mock<IConcertRepository>();
            concertRepo
                .Setup(repo => repo.GetConcert(It.IsAny<string>()))
                .ReturnsAsync(Maybe<Concert>.None);
            return concertRepo.Object;
        }

        private IConcertRepository SetupConcertRepoWithConcert()
        {
            var concertRepo = new Mock<IConcertRepository>();
            concertRepo
                .Setup(repo => repo.GetConcert(It.IsAny<string>()))
                .ReturnsAsync(Maybe<Concert>.From(new Concert("some concert", new TicketQuantity(5))));
            return concertRepo.Object;
        }

        private IBuyerRepository SetupBuyerRepoWithNoBuyer()
        {
            var buyerRepo = new Mock<IBuyerRepository>();
            buyerRepo
                .Setup(repo => repo.GetBuyer(It.IsAny<string>()))
                .ReturnsAsync(Maybe<TicketsBuyer>.None);
            return buyerRepo.Object;
        }

        private IBuyerRepository SetupBuyerRepoWithBuyer()
        {
            var buyerRepo = new Mock<IBuyerRepository>();
            buyerRepo
                .Setup(repo => repo.GetBuyer(It.IsAny<string>()))
                .ReturnsAsync(Maybe<TicketsBuyer>.From(new TicketsBuyer("1,", "some buyer")));
            return buyerRepo.Object;
        }

        private ITicketsService SetupTicketsServiceWhichSucceedsWithPurchase(Purchase purchase)
        {
            var mock = new Mock<ITicketsService>();
            mock.Setup(m => m.SellTickets(It.IsAny<IConcert>(), It.IsAny<TicketsBuyer>(), It.IsAny<TicketQuantity>()))
                .Returns(Result.Success<Purchase>(purchase));
            return mock.Object;
        }

        private ITicketsService SetupTicketsServiceWhichFailsWithError(string errorMessage)
        {
            var mock = new Mock<ITicketsService>();
            mock.Setup(m => m.SellTickets(It.IsAny<IConcert>(), It.IsAny<TicketsBuyer>(), It.IsAny<TicketQuantity>()))
                .Returns(Result.Failure<Purchase>(errorMessage));
            return mock.Object;
        }

        private Purchase BuildPurchase()
        {
            return new Purchase(new TicketsBuyer("1", "come buyer"), new Tickets(new Concert("some concert", new TicketQuantity(10)), new TicketQuantity(3)));
        }
    }
}
