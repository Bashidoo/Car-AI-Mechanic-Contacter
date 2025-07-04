using CarDealership.Application.Cars.Commands;
using CarDealership.Application.Cars.Dtos;
using CarDealership.Application.Interfaces.IAppDbContext;
using MediatR;

namespace CarDealership.Application.Cars.Handlers
{
    public class UpdatePatchCarCommandHandler : IRequestHandler<UpdatePatchCarCommand, bool>
    {
       private readonly IAppDbContext _context;
       
        public UpdatePatchCarCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdatePatchCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FindAsync(request.CarId);
            if (car == null)
                return false;

            if (!string.IsNullOrWhiteSpace(request.Model))
                car.Model = request.Model;

            if (!string.IsNullOrWhiteSpace(request.RegistrationNumber))
                car.RegistrationNumber = request.RegistrationNumber;

            if (!string.IsNullOrWhiteSpace(request.ImagePath))
                car.ImagePath = request.ImagePath;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}