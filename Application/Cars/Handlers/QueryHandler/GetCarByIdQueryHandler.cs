
using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Cars.Queries;
using CarDealership.Application.Interfaces.IAppDbContext;

using CarDealership.Application.Interfaces.CarInterface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Application.Cars.Handlers.QueryHandler
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