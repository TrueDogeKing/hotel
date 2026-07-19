namespace CampCenter.Domain.Exceptions;

/// Thrown when a requested resource does not exist (mapped to HTTP 404).
public class NotFoundException : Exception
{
    /// Creates the exception with a message describing the missing resource.
    public NotFoundException(string message)
        : base(message) { }
}
