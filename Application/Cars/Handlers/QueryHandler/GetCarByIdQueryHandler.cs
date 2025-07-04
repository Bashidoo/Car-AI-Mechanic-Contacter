using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Cars.Queries;
using CarDealership.Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Application.Cars.Handlers.QueryHandler
{
    public class GetCarsByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly IAppDbContext _context;
        
        public GetCarsByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        
        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars
                .Where(c => c.CarId == request.CarId)
                .Select(c => new CarDto
                {
                    CarId = c.CarId,
                    Model = c.Model,
                    RegistrationNumber = c.RegistrationNumber,
                    ImagePath = c.ImagePath ?? "placeholder.jpg"
                })
                .FirstOrDefaultAsync(cancellationToken);

            return car;
        }
    }
}