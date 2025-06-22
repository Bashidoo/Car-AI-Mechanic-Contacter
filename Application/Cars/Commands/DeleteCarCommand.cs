using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Cars.Commands
{
    public class DeleteCarCommand : IRequest<bool>
    {
        public int CarId { get; set; }

        public DeleteCarCommand(int carId)
        {
            CarId = carId;
        }
    }
}
