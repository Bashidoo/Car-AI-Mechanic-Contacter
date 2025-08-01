using Application.CarIssues.Commands;
using FluentValidation;

namespace Application.CarIssues.Validator
{
    public class CreateCarIssueCommandValidator : AbstractValidator<CreateCarIssueCommand>
    {
        public CreateCarIssueCommandValidator()
        {
            RuleFor(x => x.Description).NotNull().WithMessage("Description is required.");
            RuleFor(x => x.Description)
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.CarId).GreaterThan(0).WithMessage("CarId must be a positive integer.");
            RuleFor(x => x.OptionalComment)
                .MaximumLength(1000).WithMessage("Optional comment must not exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.OptionalComment));
        }
    }
}