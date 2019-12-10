using MassTransit;
using System.Threading.Tasks;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application
{
    public class TestCommandHandler : IConsumer<TestCommand>
    {
        public Task Consume(ConsumeContext<TestCommand> context)
        {
            return context.Publish(new TestEvent());
        }
    }
}
