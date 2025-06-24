using Application.CarIssues.Commands;
using Application.CarIssues.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Todo: GetCarIssueById
    //Todo: CreateCarIssue
    //Todo: UpdateCarIssue
    //Todo: DeleteCarIssue
    [ApiController]
    [Route("api/[controller]")]
    public class CarIssuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarIssuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCarIssues")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarIssuesQuery());
            return Ok(result);
        }
        
        [HttpGet("GetCarIssueById/{id}")]
        public async Task<IActionResult> GetCarIssueById(int id)
        {
            var result = await _mediator.Send(new GetCarIssueByIdQuery(id));

            if (result == null)
            {
                return NotFound($"Car issue with ID {id} not found.");
            }

            return Ok(result);
        }

        [HttpPost("CreateCarIssue")]
        public async Task<IActionResult> CreateCarIssue([FromBody] CreateCarIssueCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carIssueDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCarIssueById), new { id = carIssueDto.CarIssueId }, carIssueDto);
        }
    }
}