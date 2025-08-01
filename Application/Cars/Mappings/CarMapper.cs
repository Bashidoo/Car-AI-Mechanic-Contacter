using Application.Cars.Commands;
using Domain.Models;

namespace Application.Cars.Mappings
{
    public static class CarMapper
    {
        public static void UpdateCar(Car car, UpdateCarCommand command)
        {
            car.Model = command.Model;
            car.RegistrationNumber = command.RegistrationNumber;
            car.ImagePath = command.ImagePath ?? "placeholder.jpg";
        }
    }
}