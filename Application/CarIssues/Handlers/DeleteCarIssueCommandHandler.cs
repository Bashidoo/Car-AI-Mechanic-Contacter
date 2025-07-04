using CarDealership.Application.CarIssues.Commands;
using CarDealership.Application.Interfaces.CarIssueInterface;
using MediatR;

namespace CarDealership.Application.CarIssues.Handlers
{
    public class DeleteCarIssueCommandHandler : IRequestHandler<DeleteCarIssueCommand, bool>
    {
        private readonly ICarIssueRepository _repository;
        
        public DeleteCarIssueCommandHandler(ICarIssueRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<bool> Handle(DeleteCarIssueCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.CarIssueId, cancellationToken);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing, cancellationToken);
            return true;
        }
    }
}