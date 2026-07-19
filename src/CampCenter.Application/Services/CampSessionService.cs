using CampCenter.Application.DTOs.Sessions;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class CampSessionService : ICampSessionService
{
    private readonly ICampSessionRepository _sessions;

    public CampSessionService(ICampSessionRepository sessions) => _sessions = sessions;

    public async Task<List<CampSessionDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    ) => (await _sessions.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<CampSessionDto> CreateAsync(
        CreateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var session = new CampSession
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PricePerPersonGrosze = request.PricePerPersonGrosze,
            DepositPerPersonGrosze = request.DepositPerPersonGrosze,
            CreatedAt = DateTime.UtcNow,
        };

        await _sessions.AddAsync(session, cancellationToken);
        await _sessions.SaveChangesAsync(cancellationToken);
        return ToDto(session);
    }

    public async Task<CampSessionDto> UpdateAsync(
        Guid id,
        UpdateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var session = await GetOrThrowAsync(id, cancellationToken);

        // Dates of a session with bookings are frozen: the whole allocation and
        // pricing was made against them.
        var datesChanged =
            session.StartDate != request.StartDate || session.EndDate != request.EndDate;
        if (datesChanged && await _sessions.HasBookingsAsync(id, cancellationToken))
        {
            throw new BusinessRuleViolationException(
                "Cannot change the dates of a session that already has bookings."
            );
        }

        // A published session must never start overlapping another published one.
        if (
            datesChanged
            && session.Status == CampSessionStatus.Published
            && await _sessions.AnyPublishedOverlappingAsync(
                id,
                request.StartDate,
                request.EndDate,
                cancellationToken
            )
        )
        {
            throw new BusinessRuleViolationException(
                "The dates overlap another published session."
            );
        }

        session.Name = request.Name.Trim();
        session.StartDate = request.StartDate;
        session.EndDate = request.EndDate;
        session.PricePerPersonGrosze = request.PricePerPersonGrosze;
        session.DepositPerPersonGrosze = request.DepositPerPersonGrosze;

        await SaveWithConcurrencyCheckAsync(session, request.RowVersion, cancellationToken);
        return ToDto(session);
    }

    public async Task<CampSessionDto> PublishAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var session = await GetOrThrowAsync(id, cancellationToken);
        if (session.Status == CampSessionStatus.Published)
        {
            return ToDto(session);
        }

        if (
            await _sessions.AnyPublishedOverlappingAsync(
                id,
                session.StartDate,
                session.EndDate,
                cancellationToken
            )
        )
        {
            throw new BusinessRuleViolationException(
                "The dates overlap another published session."
            );
        }

        session.Status = CampSessionStatus.Published;
        await _sessions.SaveChangesAsync(cancellationToken);
        return ToDto(session);
    }

    public async Task<CampSessionDto> ArchiveAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var session = await GetOrThrowAsync(id, cancellationToken);
        session.Status = CampSessionStatus.Archived;
        await _sessions.SaveChangesAsync(cancellationToken);
        return ToDto(session);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var session = await GetOrThrowAsync(id, cancellationToken);
        if (await _sessions.HasBookingsAsync(id, cancellationToken))
        {
            throw new ConflictException(
                "The session has bookings and cannot be deleted; archive it instead."
            );
        }

        _sessions.Remove(session);
        await _sessions.SaveChangesAsync(cancellationToken);
    }

    private async Task<CampSession> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _sessions.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Camp session not found.");

    private async Task SaveWithConcurrencyCheckAsync(
        CampSession session,
        uint expectedRowVersion,
        CancellationToken cancellationToken
    )
    {
        if (session.RowVersion != expectedRowVersion)
        {
            throw new ConcurrencyConflictException(
                "The session was modified by someone else. Reload and try again."
            );
        }

        await _sessions.SaveChangesAsync(cancellationToken);
    }

    private static CampSessionDto ToDto(CampSession s) =>
        new(
            s.Id,
            s.Name,
            s.StartDate,
            s.EndDate,
            s.PricePerPersonGrosze,
            s.DepositPerPersonGrosze,
            s.Status.ToString(),
            s.RowVersion
        );
}
