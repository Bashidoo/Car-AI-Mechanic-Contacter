using Application.CarIssues.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Application.CarIssues.Queries
{
    public class GetAllCarIssuesQuery : IRequest<List<CarIssueDto>> { }
}