using Application.Cars.Dtos;
using Application.Cars.Queries;
using Application.Interfaces.CarInterface;
using Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Cars.Handlers.QueryHandler;
//Todo: handler ska prata med repository, inte direkt med context
public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, List<CarDto>>
{
    private readonly ICarRepository _repo;

    public GetAllCarsQueryHandler(ICarRepository repo)
    {
        _repo = repo;
    }
    

  
    public async Task<List<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _repo.GetAllAsync(cancellationToken);        // <-- rätt sätt att hämta data

        return cars
            .Select(car => new CarDto
            {
                CarId = car.CarId,
                Model = car.Model,
                RegistrationNumber = car.RegistrationNumber,
                ImagePath = car.ImagePath ?? "placeholder.jpg" // skydda mot null
            })
            .ToList();
    }
    
}

