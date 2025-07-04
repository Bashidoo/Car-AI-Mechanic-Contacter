using Application.Cars.Dtos;
using Application.Cars.Queries;
using Application.Interfaces.CarInterface;
using Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cars.Handlers.QueryHandler
{
    public class GetCarsByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly ICarRepository _repo;

        public GetCarsByIdQueryHandler(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<CarDto?> Handle(GetCarByIdQuery request, CancellationToken ct)
        {
            var car = await _repo.GetByIdAsync(request.CarId, ct);

            return car is null
                ? null
                : new CarDto
                {
                    CarId             = car.CarId,
                    Model             = car.Model,
                    RegistrationNumber= car.RegistrationNumber,
                    ImagePath         = car.ImagePath ?? "placeholder.jpg"
                };
        }

    }
}