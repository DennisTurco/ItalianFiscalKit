namespace ItalianFiscalKit.Exceptions;

/// <summary>
/// The exception that is thrown when an Italian Fiscal Code string fails validation.
/// </summary>
[Serializable]
public class InvalidFiscalCodeException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeException"/> with no message.
    /// </summary>
    public InvalidFiscalCodeException()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidFiscalCodeException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeException"/> with a descriptive message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidFiscalCodeException(string message, Exception innerException)
        : base(message, innerException)
    { }
}