using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Dtos
{
    public abstract class CarResponseDto
    {
        public int CarId { get; set; }
        public string? Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
