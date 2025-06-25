using MediatR;

namespace Application.CarIssues.Commands
{
    public class DeleteCarIssueCommand : IRequest<bool>
    {
        public int CarIssueId { get; set; }
    }
}