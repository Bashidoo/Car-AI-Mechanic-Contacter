using FluentValidation;
using Application.CarIssues.Commands;

namespace Application.CarIssues.Validators
{
    public class DeleteCarIssueCommandValidator : AbstractValidator<DeleteCarIssueCommand>
    {
        public DeleteCarIssueCommandValidator()
        {
            RuleFor(x => x.CarIssueId).GreaterThan(0).WithMessage("Invalid ID");
        }
    }
}