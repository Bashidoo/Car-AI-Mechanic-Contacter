using CarDealership.Application.CarIssues.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CarDealership.Application.CarIssues.Queries
{
    public class GetAllCarIssuesQuery : IRequest<List<CarIssueDto>> { }
}