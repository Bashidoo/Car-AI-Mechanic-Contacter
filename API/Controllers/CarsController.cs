using Application.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    //TODO: GetAllCarsByID
    //TODO: CreateCar
    //TODO: UpdateCar
    //TODO: DeleteCar
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarsQuery());
            return Ok(result);
        }

        [HttpGet("GetCarById/{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var result = await _mediator.Send(new GetCarByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}