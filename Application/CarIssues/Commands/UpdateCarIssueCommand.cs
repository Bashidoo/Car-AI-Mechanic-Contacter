using MediatR;

namespace CarDealership.Application.CarIssues.Commands
{
    public class UpdateCarIssueCommand : IRequest<bool>
    {
        public int CarIssueId { get; set; }
        public string? Description { get; set; }
        public string? OptionalComment { get; set; }
        public string? AIAnalysis { get; set; }
    }
}