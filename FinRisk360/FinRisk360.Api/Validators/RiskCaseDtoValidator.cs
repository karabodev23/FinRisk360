using FinRisk360.Application.Dtos;
using FluentValidation;

namespace FinRisk360.Api.Validators;

public class RiskCaseDtoValidator : AbstractValidator<RiskCaseDto>
{
    public RiskCaseDtoValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required.")
            .MaximumLength(100);

        RuleFor(x => x.CaseType)
            .NotEmpty()
            .WithMessage("Case type is required.")
            .MaximumLength(100);

        RuleFor(x => x.RiskLevel)
            .NotEmpty()
            .WithMessage("Risk level is required.")
            .Must(x => x == "Low" || x == "Medium" || x == "High")
            .WithMessage("Risk level must be Low, Medium, or High.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required.")
            .Must(x => x == "Open" || x == "In Progress" || x == "Closed")
            .WithMessage("Status must be Open, In Progress, or Closed.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500);
    }
}