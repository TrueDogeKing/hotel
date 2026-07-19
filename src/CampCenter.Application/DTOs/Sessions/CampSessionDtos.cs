namespace CampCenter.Application.DTOs.Sessions;

public record CampSessionDto(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze,
    string Status,
    uint RowVersion
);

public record CreateCampSessionRequestDto(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze
);

public record UpdateCampSessionRequestDto(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze,
    uint RowVersion
);
