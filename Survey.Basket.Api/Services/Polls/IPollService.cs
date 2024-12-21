using Survey.Basket.Api.Data.Entites;
using System.Threading.Tasks;

namespace Survey.Basket.Api.Servises.Polls
{
    public interface IPollService
    {
        public Task<IReadOnlyList<Poll>> GetAllAsync(CancellationToken cancellation = default);

        public Task<Poll?> GetbyIdAsync(int id, CancellationToken cancellation = default);

        public Task<Poll?> AddPollAsync(Poll poll, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellation = default);

        public Task<bool> DeleteAsync(int id, CancellationToken cancellation = default);

        public Task<bool> TogglePublishAsync(int id, CancellationToken cancellation = default);


    }
}
