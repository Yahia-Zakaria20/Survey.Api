using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Servises
{
    public class PollService : IPollService
    {
        private readonly ApplicationDbcontext _dbcontext;

        public PollService(ApplicationDbcontext dbcontext)
        {
           _dbcontext = dbcontext;
        }

        public async Task<Poll?> AddPollAsync(Poll poll)
        {
            _dbcontext.Polls.Add(poll);

           var count =  await _dbcontext.SaveChangesAsync();


            if (count > 0) {
                var result = await CountAsync();
                return await GetbyIdAsync(result);
            }

            return null;
               
        }

       
        public async Task<IReadOnlyList<Poll>> GetAllAsync()
        {
            var Polls = await _dbcontext.Polls.ToListAsync();

            return Polls;
        }

        public async Task<Poll?> GetbyIdAsync(int id)
        {
            var poll = await _dbcontext.Polls.FindAsync(id);

            return poll is not null ?  poll : null;
        }

        public async Task<Poll?> SearchByNameAsync(string name)
        {
          var poll = await  _dbcontext.Polls.FirstOrDefaultAsync(p => p.Titel == name);
            if (poll != null)
                return poll;

            return null;
        }

        public async Task<int> UpdateAsync(int id, Poll poll)
        {
            var currentpoll= await GetbyIdAsync(id);
            if(currentpoll is not  null)
                currentpoll.Titel = poll.Titel;
                currentpoll.Summary = poll.Summary;
                _dbcontext.Polls.Update(currentpoll);

            return await _dbcontext.SaveChangesAsync();


        }

        public async Task<int> DeleteAsync(int id)
        {

            var currentpoll = await GetbyIdAsync(id);
            if (currentpoll is not null)
                
            _dbcontext.Polls.Remove(currentpoll);

            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
         return await   _dbcontext.Polls.MaxAsync(p => p.Id);
        }
    }
}
