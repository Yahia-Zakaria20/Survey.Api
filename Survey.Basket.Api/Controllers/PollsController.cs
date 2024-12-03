using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Servises;

namespace Survey.Basket.Api.Controllers
{

    public class PollsController : BaseApiController
    {
        
        private readonly IPollService _service;

        public PollsController(IPollService service)
        {
         
           _service = service;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Poll>> GetAll()
        {
            
            return Ok( _service.GetAllAsync().Adapt<IEnumerable<PollDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetById([FromRoute]int id) 
        {
            var poll = await _service.GetbyIdAsync(id);

          

            return poll is null ? NotFound() : Ok(poll); 
        }

        [HttpPost]
        public async Task<ActionResult> AddPoll([FromBody]PollDto poll) 
        {
            //  return await  _service.AddPollAsync(poll) > 0 ? Ok() : BadRequest();
        
            
              var newpoll  =   await _service.AddPollAsync(poll.Adapt<Poll>());
            

            return newpoll != null ? CreatedAtAction(nameof(GetById),new {id = newpoll.Id}, newpoll.Adapt<PollDto>()) : BadRequest() ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Poll>> Update([FromRoute]int id,[FromBody] PollDto poll) 
        {
            if (id.Equals(poll.Id))
            {
                var result = await _service.UpdateAsync(id, poll.Adapt<Poll>());
                if (result > 0)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> Delete([FromRoute]int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result > 0)
                return NoContent();

            return BadRequest();

        }
        
    }
}
