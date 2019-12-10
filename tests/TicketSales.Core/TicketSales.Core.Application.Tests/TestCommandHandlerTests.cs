using MassTransit;
using Moq;
using System.Threading;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;
using Xunit;

namespace TicketSales.Core.Application
{
    public class TestCommandHandlerTests
    {
        private TestCommandHandler _sut = new TestCommandHandler();
        private Mock<ConsumeContext<TestCommand>> _consumeCtx = new Mock<ConsumeContext<TestCommand>>();

        [Fact]
        public void ConsumeTestCommand_PublishesTestEvent()
        {
            _sut.Consume(_consumeCtx.Object);
            _consumeCtx.Verify(x => x.Publish(It.IsAny<TestEvent>(), default(CancellationToken)));
        }
    }
}
