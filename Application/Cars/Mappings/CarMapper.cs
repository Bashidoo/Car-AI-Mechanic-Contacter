using CarDealership.Application.Cars.Commands;
using CarDealership.Domain.Models;

namespace CarDealership.Application.Cars.Mappings
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