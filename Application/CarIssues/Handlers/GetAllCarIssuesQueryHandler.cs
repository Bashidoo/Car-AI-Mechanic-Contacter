using CarDealership.Application.CarIssues.Dtos;
using CarDealership.Application.CarIssues.Queries;
using CarDealership.Application.Interfaces.CarIssueInterface;
using AutoMapper;
using CarDealership.Domain.Models;
using MediatR;

namespace CarDealership.Application.CarIssues.Handlers
{
    public class GetAllCarIssuesQueryHandler : IRequestHandler<GetAllCarIssuesQuery, List<CarIssueDto>>
    {
        private readonly ICarIssueRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCarIssuesQueryHandler(ICarIssueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CarIssueDto>> Handle(GetAllCarIssuesQuery request, CancellationToken cancellationToken)
        {
            var carIssues = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<CarIssueDto>>(carIssues);
        }
    }
}