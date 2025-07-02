using Application.CarIssues.Dtos;
using MediatR;

namespace Application.CarIssues.Commands
{
    public class CreateCarIssueCommand : IRequest<CarIssueDto>
    {
        public string Description { get; set; }
        public string? OptionalComment { get; set; }
        public string? AIAnalysis { get; set; }
        public int CarId { get; set; }
    }
}