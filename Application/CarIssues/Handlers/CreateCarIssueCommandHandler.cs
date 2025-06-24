using Application.CarIssues.Commands;
using Application.CarIssues.Dtos;
using Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CarIssues.Handlers
{
    public class CreateCarIssueCommandHandler : IRequestHandler<CreateCarIssueCommand, CarIssueDto>
    {
       private readonly IAppDbContext _context;
       
        public CreateCarIssueCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CarIssueDto> Handle(CreateCarIssueCommand request, CancellationToken cancellationToken)
        {
            var carExists = await _context.Cars.AnyAsync(c => c.CarId == request.CarId, cancellationToken);
            if (!carExists)
                throw new KeyNotFoundException($"Car with ID {request.CarId} does not exist.");
            
            var carIssue = new Domain.Models.CarIssue
            {
                Description = request.Description,
                OptionalComment = request.OptionalComment,
                AIAnalysis = request.AIAnalysis,
                CarId = request.CarId,
                CreatedAt = DateTime.UtcNow
            };

            _context.CarIssues.Add(carIssue);
            await _context.SaveChangesAsync(cancellationToken);

            return new CarIssueDto
            {
                CarIssueId = carIssue.CarIssueId,
                Description = carIssue.Description,
                OptionalComment = carIssue.OptionalComment,
                AIAnalysis = carIssue.AIAnalysis,
                CreatedAt = carIssue.CreatedAt,
                CarId = carIssue.CarId
            };
        }
        
    }
}