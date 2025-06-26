using Application.Cars.Dtos;
using Application.Cars.Queries;
using Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cars.Handlers.QueryHandler
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