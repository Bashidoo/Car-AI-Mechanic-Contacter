using Application.Cars.Commands;
using FluentValidation;

namespace Application.Cars.Validator
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(100);

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required.")
                .MaximumLength(7).WithMessage("Maximum tecken is 7.");

            RuleFor(x => x.ImagePath)
                .MaximumLength(255);
        }
    }
}