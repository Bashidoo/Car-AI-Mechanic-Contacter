using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Cars.Dtos;
using MediatR;

namespace Application.Cars.Queries
{
    public record GetCarByIdQuery(int CarId) : IRequest<CarDto?>;
  

}
