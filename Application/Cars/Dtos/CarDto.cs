using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Application.Cars.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string? Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
