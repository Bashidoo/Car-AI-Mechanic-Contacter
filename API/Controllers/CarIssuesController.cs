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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarIssuesQuery());
            return Ok(result);
        }
    }
}