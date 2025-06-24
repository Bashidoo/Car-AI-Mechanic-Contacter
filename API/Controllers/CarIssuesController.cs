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

    }
}