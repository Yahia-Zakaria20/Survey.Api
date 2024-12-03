using Survey.Basket.Api.Data.Entites;
using System.Threading.Tasks;

namespace Survey.Basket.Api.Servises
{
    public interface IPollService
    {
        public Task<IReadOnlyList<Poll>> GetAllAsync();

        public Task<Poll?> GetbyIdAsync(int id);

        public Task<Poll?> AddPollAsync(Poll poll);

        public Task<Poll?> SearchByNameAsync(string name);

        public Task<int> UpdateAsync(int id,Poll poll);

        public Task<int> DeleteAsync(int id);

        public Task<int> CountAsync();
    }
}
