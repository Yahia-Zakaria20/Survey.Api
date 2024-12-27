using Mapster;
using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Servises.Polls
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
            await _dbcontext.Polls.AddAsync(poll, cancellation);

            var count = await _dbcontext.SaveChangesAsync(cancellation);

            return count > 0 ? poll : null;

        }


        public async Task<IReadOnlyList<PollDto>> GetAllAsync(CancellationToken cancellation) // becouse i dont need to TRACKING FOR THIS DATA 
        {
            var Polls = await _dbcontext.Polls.AsNoTracking().ToListAsync(cancellation);

            return Polls.Adapt<IReadOnlyList<PollDto>>();
        }

        public async Task<PollDto?> GetbyIdAsync(int id, CancellationToken cancellation)
        {
            var poll = await _dbcontext.Polls.FindAsync(id, cancellation);

            var PollDto = poll.Adapt<PollDto>();

            return poll is not null ?  PollDto : null;
        }



        public async Task<bool> UpdateAsync(int id, PollDto polldtoRequest, CancellationToken cancellation)
        {
            var currentpoll =/* await GetbyIdAsync(id,cancellation)*/ await  _dbcontext.Polls.FindAsync(id);

           //var  currentpoll =  pollDto.Adapt<Poll>();
            if (currentpoll is not null)
            currentpoll.Titel = polldtoRequest.Titel;
            currentpoll!.Summary = polldtoRequest.Summary;
            currentpoll.StartsAt = polldtoRequest.StartsAt;
            currentpoll.EndsAt = polldtoRequest.EndsAt;

            _dbcontext.Polls.Update(currentpoll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0;


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
        {

            var currentpoll = await _dbcontext.Polls.FindAsync(id);
            if (currentpoll is not null)

                _dbcontext.Polls.Remove(currentpoll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<bool> TogglePublishAsync(int id, CancellationToken cancellation = default)
        {
            var poll = await _dbcontext.Polls.FindAsync(id); 

            poll!.IsPublished = !poll.IsPublished;

            _dbcontext.Update(poll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0;
        }
    }
}
