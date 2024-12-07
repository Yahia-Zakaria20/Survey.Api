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

        public async Task<Poll?> AddPollAsync(Poll poll, CancellationToken cancellation)
        {
          await  _dbcontext.Polls.AddAsync(poll,cancellation);

            var count =  await _dbcontext.SaveChangesAsync(cancellation);

              return count > 0 ?  poll : null ;
               
        }

       
        public async Task<IReadOnlyList<Poll>> GetAllAsync(CancellationToken cancellation) // becouse i dont need to TRACKING FOR THIS DATA 
        {
            var Polls = await _dbcontext.Polls.AsNoTracking().ToListAsync(cancellation);

            return Polls;
        }

        public async Task<Poll?> GetbyIdAsync(int id, CancellationToken cancellation)
        {
            var poll = await _dbcontext.Polls.FindAsync(id,cancellation);

            return poll is not null ?  poll : null;
        }

      

        public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellation)
        {
            var currentpoll= await GetbyIdAsync(id,cancellation);
            if(currentpoll is not  null)
                currentpoll.Titel = poll.Titel;
                currentpoll!.Summary = poll.Summary;
                currentpoll.StartsAt = poll.StartsAt;
                currentpoll.EndsAt = poll.EndsAt;
                _dbcontext.Polls.Update(currentpoll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0;


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
        {

            var currentpoll = await GetbyIdAsync(id,cancellation);
            if (currentpoll is not null)
                
            _dbcontext.Polls.Remove(currentpoll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0 ;
        }

        public async Task<bool> ToggleSatutsAsync(int id, CancellationToken cancellation = default)
        {
          var poll =  await GetbyIdAsync(id , cancellation);

            poll.IsPublished = !poll.IsPublished;

            _dbcontext.Update(poll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0;
        }
    }
}
