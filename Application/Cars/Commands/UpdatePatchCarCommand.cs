using MediatR;

namespace CarDealership.Application.Cars.Commands
{
    public class UpdatePatchCarCommand : IRequest<bool>
    {
        public int CarId { get; set; }
        public string? Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}