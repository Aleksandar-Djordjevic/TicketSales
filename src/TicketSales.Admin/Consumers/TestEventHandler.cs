using MassTransit;
using System;
using System.Threading.Tasks;
using TicketSales.Admin.Services;
using TicketSales.Messages.Events;

namespace TicketSales.Admin.Consumers
{
    public class TestEventHandler : IConsumer<TestEvent>
    {
        private readonly TestMessageStore _store;

        public TestEventHandler(TestMessageStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public Task Consume(ConsumeContext<TestEvent> context)
        {
            _store.IncrementCount();
            return Task.CompletedTask;
        }
    }
}
