using System.Threading.Tasks;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Application.UseCases.CreateConcert
{
    public class CreateConcertService : ICreateConcertService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IPublishEvents _eventPublisher;

        public CreateConcertService(IConcertRepository concertRepository, IPublishEvents eventPublisher)
        {
            _concertRepository = concertRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task CreateConcert(CreateConcertRequest request)
        {
            var concert = new Concert(request.Name, new TicketQuantity(request.SeatingCapacity));
            await _concertRepository.AddConcert(concert);
            await _eventPublisher.Publish(new ConcertCreatedEvent(concert.Id, concert.Name, concert.SeatingCapacity));
        }
    }
}
