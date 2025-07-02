using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Cars.Commands;
using Application.Interfaces.IAppDbContext;
using MediatR;

namespace Application.Cars.Handlers
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly IAppDbContext _context;
        public DeleteCarCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FindAsync(request.CarId);
            if (car == null)
                return false;
            
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
