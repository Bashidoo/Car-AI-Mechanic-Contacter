using CarDealership.Application.CarIssues.Dtos;
using MediatR;

namespace CarDealership.Application.CarIssues.Queries
{
    public class GetCarIssueByIdQuery : IRequest<CarIssueDto>
    {
        public int CarIssueId { get; set; }

        public GetCarIssueByIdQuery(int carIssueId)
        {
            CarIssueId = carIssueId;
        }
    }

}