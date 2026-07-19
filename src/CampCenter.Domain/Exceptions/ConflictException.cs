namespace CampCenter.Domain.Exceptions;

/// Signals that a unique value (login or email) is already taken. Mapped to HTTP 409.
public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message) { }
}
