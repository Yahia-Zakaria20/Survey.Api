using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Servises.Polls;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Survey.Basket.Api.Controllers
{
   
    [Authorize]
    public class PollsController : BaseApiController
    {

        private readonly IPollService _service;

        public PollsController(IPollService service)
        {

            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Poll>>> GetAll(CancellationToken cancellation)
        {
            var result = await _service.GetAllAsync(cancellation);

            return Ok(result.Adapt<IReadOnlyList<PollDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetById([FromRoute] int id, CancellationToken cancellation)
        {
            var poll = await _service.GetbyIdAsync(id, cancellation);



            return poll is null ? NotFound() : Ok(poll);
        }

        [HttpPost]
        public async Task<ActionResult> AddPoll([FromBody] PollDto poll, CancellationToken cancellation)
        {
            //  return await  _service.AddPollAsync(poll) > 0 ? Ok() : BadRequest();
         var userid =  User.FindFirstValue(JwtRegisteredClaimNames.Sub);


            var newpoll = await _service.AddPollAsync(poll.Adapt<Poll>(), cancellation);


            return newpoll != null ? CreatedAtAction(nameof(GetById), new { id = newpoll.Id }, newpoll.Adapt<PollDto>()) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Poll>> Update([FromRoute] int id, [FromBody] PollDto poll, CancellationToken cancellation)
        {
            if (id.Equals(poll.Id))
            {
                var result = await _service.UpdateAsync(id, poll.Adapt<Poll>(), cancellation);
                if (result)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await _service.DeleteAsync(id, cancellation);
            if (result)
                return NoContent();

            return BadRequest();

        }

        [HttpPut("{id}/TogglePublish")]
        public async Task<ActionResult<Poll>> TogglePublish([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await _service.TogglePublishAsync(id, cancellation);
            if (result)
                return NoContent();

            return BadRequest();
        }

     
    }
}
