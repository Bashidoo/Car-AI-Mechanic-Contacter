using MediatR;

namespace CarDealership.Application.CarIssues.Commands
{
    public class DeleteCarIssueCommand : IRequest<bool>
    {
        public int CarIssueId { get; set; }
    }
}