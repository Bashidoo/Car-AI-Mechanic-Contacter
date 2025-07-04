using CarDealership.Application.Cars.Commands;
using CarDealership.Application.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
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
        
        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCarById), new { id = carId }, null);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarCommand command)
        {
            if (id != command.CarId)
                return BadRequest("ID in URL does not match ID in request body");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCar(int id, [FromBody] UpdatePatchCarCommand command)
        {
            if (id != command.CarId)
                return BadRequest("ID mismatch");

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return Ok("Car updated.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _mediator.Send(new DeleteCarCommand(id));

            if (!result)
                return NotFound();

            return NoContent(); // 204 – lyckad radering utan innehåll
        }



    }
}