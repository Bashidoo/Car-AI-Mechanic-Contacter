using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Application.Cars.Dtos;
using MediatR;

namespace CarDealership.Application.Cars.Commands
{
    public class UpdateCarCommand : IRequest<CarDto>
    {
        public int CarId { get; set; }
        public string? Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
