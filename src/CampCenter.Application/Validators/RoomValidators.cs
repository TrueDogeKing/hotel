using CampCenter.Application.DTOs.Rooms;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequestDto>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Capacity).InclusiveBetween(1, 20);
        RuleFor(x => x.Description).MaximumLength(512);
    }
}

public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequestDto>
{
    public UpdateRoomRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Capacity).InclusiveBetween(1, 20);
        RuleFor(x => x.Description).MaximumLength(512);
    }
}
