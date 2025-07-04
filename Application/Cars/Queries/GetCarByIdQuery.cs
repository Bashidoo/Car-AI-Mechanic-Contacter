using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Application.Cars.Dtos;
using MediatR;

namespace CarDealership.Application.Cars.Queries
{
    public class GetCarByIdQuery : IRequest<CarDto>
    {
        public int CarId { get; set; }

        public GetCarByIdQuery(int carId)
        {
            CarId = carId;
        }
    }
}
