using FluentValidation;
using CarDealership.Application.CarIssues.Commands;

namespace CarDealership.Application.CarIssues.Validators
{
    public class DeleteCarIssueCommandValidator : AbstractValidator<DeleteCarIssueCommand>
    {
        public DeleteCarIssueCommandValidator()
        {
            RuleFor(x => x.CarIssueId).GreaterThan(0).WithMessage("Invalid ID");
        }
    }
}