using CarDealership.Application.CarIssues.Commands;
using CarDealership.Application.Interfaces.CarIssueInterface;
using AutoMapper;
using MediatR;

namespace CarDealership.Application.CarIssues.Handlers
{
    public class UpdateCarIssueCommandHandler : IRequestHandler<UpdateCarIssueCommand, bool>
    {
        private readonly ICarIssueRepository _carIssueRepository;
        private readonly IMapper _mapper;
        
        public UpdateCarIssueCommandHandler(ICarIssueRepository carIssueRepository, IMapper mapper)
        {
            _carIssueRepository = carIssueRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateCarIssueCommand request, CancellationToken cancellationToken)
        {
            // 1. Hämta befintlig CarIssue från databasen via repository
            var issue = await _carIssueRepository.GetByIdAsync(request.CarIssueId, cancellationToken);
            if (issue == null)
            {
                return false; // Om inte hittad, returnera false
            }

            // 2. Uppdatera fält via AutoMapper
            _mapper.Map(request, issue); // Den uppdaterar bara de fält som finns i command och mappas

            // 3. Spara ändringen via repository
            await _carIssueRepository.UpdateAsync(issue, cancellationToken);

            return true;
        }

        
    }
}