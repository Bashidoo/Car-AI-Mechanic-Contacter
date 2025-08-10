using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }
        [StringLength(100, ErrorMessage = "Model cannot exceed 100 characters")]
        public string? Model { get; set; }
        [StringLength(8, ErrorMessage = "Registration number cannot exceed 8 characters")]
        public string? RegistrationNumber { get; set; }
        [StringLength(255, ErrorMessage = "Image path cannot exceed 255 characters")]
        public string? ImagePath { get; set; }
    }
}
