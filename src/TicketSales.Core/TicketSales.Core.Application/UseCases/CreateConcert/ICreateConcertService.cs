using System.Threading.Tasks;

namespace Application.CreateConcert
{
    public interface ICreateConcertService
    {
        Task CreateConcert(CreateConcertRequest request);
    }
}