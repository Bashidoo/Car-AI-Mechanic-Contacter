using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Application.Cars.Commands;
using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Cars.Mappings;
using CarDealership.Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Application.Cars.Handlers
{
    public class UpdateCarCommandHandler : IRequestHandler< UpdateCarCommand, CarDto>
    {
        private readonly IAppDbContext _context;
        
        public UpdateCarCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == request.CarId, cancellationToken);
            if (car == null)
                return null;

           CarMapper.UpdateCar(car, request);
            await _context.SaveChangesAsync(cancellationToken);

            return new CarDto
            {
                CarId = car.CarId,
                Model = car.Model,
                RegistrationNumber = car.RegistrationNumber,
                ImagePath = car.ImagePath
            };
        }
    }
}
