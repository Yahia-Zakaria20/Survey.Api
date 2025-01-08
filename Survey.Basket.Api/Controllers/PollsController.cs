using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Error;
using Survey.Basket.Api.Errors;
using Survey.Basket.Api.Extentions;
using Survey.Basket.Api.Servises.Polls;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
        public async Task<ActionResult<IReadOnlyList<PollDto>>> GetAll(CancellationToken cancellation)
        {
            var result = await _service.GetAllAsync(cancellation);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PollDto>> GetById([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await _service.GetbyIdAsync(id, cancellation);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost]
        public async Task<ActionResult<PollDto>> AddPoll([FromBody] PollDto poll, CancellationToken cancellation)
        {
            //  return await  _service.AddPollAsync(poll) > 0 ? Ok() : BadRequest(); 
            //409 Conflict


            var result = await _service.AddPollAsync(poll, cancellation);


            //   return newpoll != null ? CreatedAtAction(nameof(GetById), new { id = newpoll.Id }, newpoll.Adapt<PollDto>()) : BadRequest(new ApiResponse(StatusCodes.Status409Conflict));

              return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] PollDto poll, CancellationToken cancellation)
        {          
            var result = await _service.UpdateAsync(id, poll, cancellation);

                if (result.IsSuccess)
                    return NoContent();
            return result.ToProblem();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await _service.DeleteAsync(id, cancellation);
            if (result.IsSuccess)
                return NoContent();

            return result.ToProblem();

        }

        [HttpPut("{id}/TogglePublish")]
        public async Task<ActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await _service.TogglePublishAsync(id, cancellation);
            if (result.IsSuccess)
                return NoContent();

            return result.ToProblem();
        }


    }
}
