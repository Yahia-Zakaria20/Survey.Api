
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Errors;
using System.Threading.Tasks;

namespace Survey.Basket.Api.Servises.Polls
{
    public interface IPollService
    {
        public Task<IReadOnlyList<PollDto>> GetAllAsync(CancellationToken cancellation = default);

        public Task<Result<PollDto>> GetbyIdAsync(int id, CancellationToken cancellation = default);

        public Task<Result<PollDto>> AddPollAsync(PollDto poll, CancellationToken cancellation = default);

        public Task<Result> UpdateAsync(int id, PollDto polldto, CancellationToken cancellation = default);

        public Task<Result> DeleteAsync(int id, CancellationToken cancellation = default);

        public Task<Result> TogglePublishAsync(int id, CancellationToken cancellation = default);


    }
}
