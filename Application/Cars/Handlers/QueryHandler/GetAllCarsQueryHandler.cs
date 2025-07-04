using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Cars.Queries;
using CarDealership.Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CarDealership.Application.Cars.Handlers.QueryHandler;

public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, List<CarDto>>
{
    private readonly IAppDbContext _context;

    public GetAllCarsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

  
    public async Task<List<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cars
            .Select(car => new CarDto
            {
                CarId = car.CarId,
                Model = car.Model,
                RegistrationNumber = car.RegistrationNumber,
                ImagePath = car.ImagePath ?? "placeholder.jpg" // skydda mot null
            })
            .ToListAsync(cancellationToken);
    }
    
}

