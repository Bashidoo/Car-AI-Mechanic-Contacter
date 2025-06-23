using Application.CarIssues.Dtos;
using Application.CarIssues.Queries;
using Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CarIssues.Handlers
{
    public class GetAllCarIssuesQueryHandler : IRequestHandler<GetAllCarIssuesQuery, List<CarIssueDto>>
    {
        private readonly IAppDbContext _context;

        public GetAllCarIssuesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarIssueDto>> Handle(GetAllCarIssuesQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarIssues
                .Select(issue => new CarIssueDto
                {
                    CarIssueId = issue.CarIssueId,
                    Description = issue.Description,
                    OptionalComment = issue.OptionalComment,
                    AIAnalysis = issue.AIAnalysis,
                    CreatedAt = issue.CreatedAt,
                    CarId = issue.CarId
                })
                .ToListAsync(cancellationToken);
        }
    }
}