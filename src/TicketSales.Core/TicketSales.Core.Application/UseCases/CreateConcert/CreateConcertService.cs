using Application.Ports;
using Domain.Models;
using System.Threading.Tasks;
using TicketSales.Core.Domain.Events;

namespace Application.CreateConcert
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
            await _eventPublisher.Publish(new ConcertCreatedEvent(concert.Name, concert.SeatingCapacity));
        }
    }
}
