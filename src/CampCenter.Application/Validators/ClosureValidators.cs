using CampCenter.Application.DTOs.Closures;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateClosureRequestValidator : AbstractValidator<CreateClosureRequestDto>
{
    public CreateClosureRequestValidator()
    {
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(256);
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date cannot be before the start date.");
    }
}

public class UpdateClosureRequestValidator : AbstractValidator<UpdateClosureRequestDto>
{
    public UpdateClosureRequestValidator()
    {
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(256);
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date cannot be before the start date.");
    }
}
