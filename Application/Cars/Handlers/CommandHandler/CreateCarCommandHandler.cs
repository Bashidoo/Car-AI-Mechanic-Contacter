using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Application.Cars.Commands;
using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Interfaces.IAppDbContext;
using CarDealership.Domain.Models;
using MediatR;

namespace CarDealership.Application.Cars.Handlers
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarDto>
    {
        private readonly IAppDbContext _context;

        public CreateCarCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                Model = request.Model,
                RegistrationNumber = request.RegistrationNumber,
                ImagePath = request.ImagePath
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync(cancellationToken);

            return new CarDto
            {
                CarId = car.CarId,
                Model = car.Model,
                RegistrationNumber = car.RegistrationNumber,
                ImagePath = car.ImagePath ?? "placeholder.jpg"
            };
        }
    }
    
}

