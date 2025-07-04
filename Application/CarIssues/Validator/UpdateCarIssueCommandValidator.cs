using CarDealership.Application.CarIssues.Commands;
using FluentValidation;

namespace CarDealership.Application.CarIssues.Validator
{
    public class UpdateCarIssueCommandValidator : AbstractValidator<UpdateCarIssueCommand>
    {
     public UpdateCarIssueCommandValidator()   
        {
            RuleFor(x => x.CarIssueId)
                .GreaterThan(0).WithMessage("Car Issue ID must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OptionalComment)
                .MaximumLength(1000).WithMessage("Optional comment cannot exceed 1000 characters.");

            RuleFor(x => x.AIAnalysis)
                .MaximumLength(1000).WithMessage("AI analysis cannot exceed 1000 characters.");
        }
    }
}