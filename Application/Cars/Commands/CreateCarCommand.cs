using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Cars.Dtos;
using MediatR;

namespace Application.Cars.Commands
{
    public class CreateCarCommand : IRequest<CarDto>
    {
        public string? Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImagePath { get; set; }

        public CreateCarCommand(string? model, string? registrationNumber, string? imagePath)
        {
            Model = model;
            RegistrationNumber = registrationNumber;
            ImagePath = imagePath;
        }
    }
}
