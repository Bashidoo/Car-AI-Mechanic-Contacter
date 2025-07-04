
﻿using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Cars.Queries;
using CarDealership.Application.Interfaces.IAppDbContext;
using CarDealership.Application.Interfaces.IAppDbContext;

using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CarDealership.Application.Cars.Handlers.QueryHandler;

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

