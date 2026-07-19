using CampCenter.Application.DTOs.Sessions;
using FluentValidation;

namespace CampCenter.Application.Validators;

public static class CampSessionRules
{
    public static IRuleBuilderOptions<T, DateOnly> AfterStart<T>(
        this IRuleBuilder<T, DateOnly> rule,
        Func<T, DateOnly> start
    ) =>
        rule.Must((dto, end) => end > start(dto))
            .WithMessage("End date must be after the start date.");
}

public class CreateCampSessionRequestValidator : AbstractValidator<CreateCampSessionRequestDto>
{
    public CreateCampSessionRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
        RuleFor(x => x.EndDate).AfterStart(x => x.StartDate);
        RuleFor(x => x.PricePerPersonGrosze).GreaterThan(0);
        RuleFor(x => x.DepositPerPersonGrosze).GreaterThan(0);
        RuleFor(x => x.DepositPerPersonGrosze)
            .LessThanOrEqualTo(x => x.PricePerPersonGrosze)
            .WithMessage("Deposit per person cannot exceed the price per person.");
    }
}

public class UpdateCampSessionRequestValidator : AbstractValidator<UpdateCampSessionRequestDto>
{
    public UpdateCampSessionRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
        RuleFor(x => x.EndDate).AfterStart(x => x.StartDate);
        RuleFor(x => x.PricePerPersonGrosze).GreaterThan(0);
        RuleFor(x => x.DepositPerPersonGrosze).GreaterThan(0);
        RuleFor(x => x.DepositPerPersonGrosze)
            .LessThanOrEqualTo(x => x.PricePerPersonGrosze)
            .WithMessage("Deposit per person cannot exceed the price per person.");
    }
}
