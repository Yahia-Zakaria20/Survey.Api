using Mapster;
using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Error;
using Survey.Basket.Api.Errors;

namespace Survey.Basket.Api.Servises.Polls
{
    public class PollService : IPollService
    {
        private readonly ApplicationDbcontext _dbcontext;

        public PollService(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<PollDto>> AddPollAsync(PollDto poll, CancellationToken cancellation)
        {
            var IsExist = await _dbcontext.Polls.CountAsync(p => p.Titel == poll.Titel, cancellation);

           if (IsExist > 0) 
                return Result<PollDto>.Falier(new ApiResponse(409)); // Set Is sucess = false

            await _dbcontext.Polls.AddAsync(poll.Adapt<Poll>(), cancellation);

          var count =  await _dbcontext.SaveChangesAsync(cancellation);

            return count > 0 ? Result<PollDto>.Success(poll) : Result<PollDto>.Falier(new ApiResponse(400));// set is Sucess  = true    
        }


        public async Task<IReadOnlyList<PollDto>> GetAllAsync(CancellationToken cancellation) // becouse i dont need to TRACKING FOR THIS DATA 
        {
            var Polls = await _dbcontext.Polls.AsNoTracking().ToListAsync(cancellation);

            return Polls.Adapt<IReadOnlyList<PollDto>>();
        }

        public async Task<Result<PollDto>> GetbyIdAsync(int id, CancellationToken cancellation)
        {
            var poll = await _dbcontext.Polls.FindAsync(id, cancellation);

            //   var polldto = 
            if (poll is not null)
                return Result<PollDto>.Success(poll.Adapt<PollDto>());



            return  Result<PollDto>.Falier(new ApiResponse(404));
        }



        public async Task<Result> UpdateAsync(int id, PollDto polldtoRequest, CancellationToken cancellation)
        {

            if (id.Equals(polldtoRequest.Id))
            {
                var count =await _dbcontext.Polls.CountAsync(p => p.Titel == polldtoRequest.Titel && p.Id != polldtoRequest.Id,cancellation);
                if(count > 0)
                   return Result.Falier(new ApiResponse(409));
                var currentpoll =/* await GetbyIdAsync(id,cancellation)*/ await _dbcontext.Polls.FindAsync(id);
                //var  currentpoll =  pollDto.Adapt<Poll>();
                if (currentpoll is not null)
                        currentpoll.Titel = polldtoRequest.Titel;
                        currentpoll!.Summary = polldtoRequest.Summary;
                        currentpoll.StartsAt = polldtoRequest.StartsAt;
                        currentpoll.EndsAt = polldtoRequest.EndsAt;

                _dbcontext.Polls.Update(currentpoll);

                if (await _dbcontext.SaveChangesAsync(cancellation) > 0)
                    return Result.Success();

            }

            return Result.Falier(new ApiResponse(400));
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellation)
        {

            var currentpoll = await _dbcontext.Polls.FindAsync(id);
            if (currentpoll is not null)

                _dbcontext.Polls.Remove(currentpoll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0 ? Result.Success() :  Result.Falier(new ApiResponse(400)) ;
        }

        public async Task<Result> TogglePublishAsync(int id, CancellationToken cancellation = default)
        {
            var poll = await _dbcontext.Polls.FindAsync(id); 

            poll!.IsPublished = !poll.IsPublished;

            _dbcontext.Update(poll);

            return await _dbcontext.SaveChangesAsync(cancellation) > 0 ? Result.Success() : Result.Falier(new ApiResponse(400));
        }
    }
}
